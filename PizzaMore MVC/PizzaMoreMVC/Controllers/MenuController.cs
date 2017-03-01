using AutoMapper;
using PizzaMoreMVC.BindingModels;
using PizzaMoreMVC.Data;
using PizzaMoreMVC.Models;
using PizzaMoreMVC.Security;
using PizzaMoreMVC.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using System.Linq;

namespace PizzaMoreMVC.Controllers
{
    public class MenuController:Controller
    {
        private SignInManager signInManager;

        public MenuController()
        {
            this.signInManager = new SignInManager(Data.Data.Context);
        }

        [HttpGet]
        public IActionResult<PizzaSuggestionModel> Index(HttpSession session)
        {
            if (!this.signInManager.IsAuthenticated(session))
            {
                return this.View(new PizzaSuggestionModel(),"Home", "Index");
            }
            using(PizzaMvcContext context = new PizzaMvcContext())
            {
                User currentUser = context.Sessions
                    .First(s => s.SessionId == session.Id)
                    .User;
                PizzaSuggestionModel viewModel = new PizzaSuggestionModel()
                {
                    Email = currentUser.Email,
                    PizzaSuggestions = currentUser.PizzaSuggestions
                };
                return this.View(viewModel);
            }            
        }

        [HttpPost]
        public IActionResult<PizzaSuggestionModel> Index(VotePizzaBindingModel model, HttpSession session, HttpResponse response)
        {
            using (PizzaMvcContext context = new PizzaMvcContext())
            {
                User currentUser = context.Sessions.First(s => s.SessionId == session.Id).User;
                PizzaSuggestionModel viewModel = new PizzaSuggestionModel()
                {
                    Email = currentUser.Email,
                    PizzaSuggestions = currentUser.PizzaSuggestions
                };

                Pizza currentPizza = context.Pizzas.Find(model.PizzaId);
                if (model.PizzaVote == "Up")
                {
                    currentPizza.UpVotes++;
                }else if (model.PizzaVote == "Down")
                {
                    currentPizza.DownVotes++;
                }
                context.SaveChanges();
                this.Redirect(response, "/menu/index");
                return null;
            }
        }


        [HttpGet]
        public IActionResult Add(HttpSession session)
        {
            if (!this.signInManager.IsAuthenticated(session))
            {
                return this.View("Users", "Signin");
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddPizzaBindingModel model, HttpSession session, HttpResponse response)
        {
            if (!this.signInManager.IsAuthenticated(session))
            {
                this.Redirect(response, "/users/signin");
                return null;
            }

            using (PizzaMvcContext context = new PizzaMvcContext())
            {
                ConfigureMapper(session, context);
                Pizza pizzaEntity = Mapper.Map<Pizza>(model);
                context.Pizzas.Add(pizzaEntity);
                context.SaveChanges();
            }

            this.Redirect(response, "/menu/index");
            return null;
        }
        [HttpGet]
        public IActionResult<PizzasViewModel> Suggestions(HttpSession session, HttpResponse response)
        {
            if (!this.signInManager.IsAuthenticated(session))
            {
                this.Redirect(response, "/users/signin");
            }

            using (PizzaMvcContext context = new PizzaMvcContext())
            {
                User currentUser = context.Sessions.First(s => s.SessionId == session.Id).User;
                PizzasViewModel viewModel = new PizzasViewModel()
                {
                    PizzaSuggestions = currentUser.PizzaSuggestions.ToList()
                };
                return this.View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<PizzasViewModel> Suggestions(DeletePizzaBindingModel model, HttpSession session, HttpResponse response)
        {
            using (PizzaMvcContext context = new PizzaMvcContext())
            {
                Pizza pizzaEntity = context.Pizzas.Find(model.PizzaId);
                context.Pizzas.Remove(pizzaEntity);
                context.SaveChanges();

                User currentUser = context.Sessions.First(s => s.SessionId == session.Id).User;
                PizzasViewModel viewModel = new PizzasViewModel()
                {
                    PizzaSuggestions = currentUser.PizzaSuggestions.ToList()
                };


                this.Redirect(response, "/menu/suggestions");
                return null;
            }
        }

        [HttpGet]
        public IActionResult<DetailsViewModel> DetailsPizza(int pizzaId)
        {
            using (PizzaMvcContext context = new PizzaMvcContext())
            {
                Pizza pizzaEntity = context.Pizzas.Find(pizzaId);
                DetailsViewModel viewModel = new DetailsViewModel()
                {
                    Title = pizzaEntity.Title,
                    Recipe = pizzaEntity.Recipe,
                    ImageUrl = pizzaEntity.ImageUrl,
                    UpVotes = pizzaEntity.UpVotes,
                    DownVotes = pizzaEntity.DownVotes
                };

                return this.View(viewModel);
            }
        }

        private void ConfigureMapper(HttpSession session, PizzaMvcContext context)
        {
            Mapper.Initialize(
                expression => expression.CreateMap<AddPizzaBindingModel, Pizza>()
                .ForMember(p => p.Owner, config => config
                .MapFrom(u => context.Sessions.First(s => s.SessionId == session.Id).User))
                );
        }
    }
}
