using System.Collections.Generic;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;
using PizzaForumApp.Services;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Controllers
{
    public class TopicsController: Controller
    {
        private TopicsService service;

        public TopicsController()
        {
            this.service = new TopicsService();
        }
        [HttpGet]
        public IActionResult<IEnumerable<string>> New(HttpSession session, HttpResponse response)
        {
            User user = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (user == null)
            {
                this.Redirect(response, "/home/topics");
            }

            IEnumerable<string> categories = this.service.GetCategoryNames();
            return this.View(categories);
        }

        [HttpPost]
        public IActionResult New(HttpSession session, HttpResponse response, TopicBindingModel model)
        {
            User user = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (user == null)
            {
                this.Redirect(response, "/home/topics");
            }

            if (!this.service.IsNewTopicValid(model))
            {
                this.Redirect(response, "/topics/new");
            }
            this.service.AddNewTopic(model, user);
            this.Redirect(response, "/home/topics");
            return null;
        }

        [HttpGet]
        public IActionResult<DetailsViewModel> Details(HttpSession session, HttpResponse response, int id)
        {
            User user = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (user == null)
            {
                this.Redirect(response, "/home/topics");
            }

            DetailsViewModel viewModel = this.service.GetDetailsVm(id);
            return this.View(viewModel);
        }
    }
}
