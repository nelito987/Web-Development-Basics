using PizzaMoreMVC.Data;
using PizzaMoreMVC.Models;
using PizzaMoreMVC.Security;
using PizzaMoreMVC.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
