using System.Net;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;
using PizzaForumApp.Services;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;

namespace PizzaForumApp.Controllers
{
    public class ForumController : Controller
    {
        private ForumService service;

        public ForumController()
        {
            this.service = new ForumService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model, HttpResponse response)
        {
            if (!this.service.IsViewModelValid(model))
            {
                this.Redirect(response, "/forum/register");
                return null;
            }

            User user = this.service.GetUserFromBind(model);
            this.service.RegisterUser(user);
            this.Redirect(response, "/forum/login");
            return null;
        }
    }
}
