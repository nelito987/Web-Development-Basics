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
    public class ForumController : Controller
    {
        private ForumService service;

        public ForumController()
        {
            this.service = new ForumService();
        }

        [HttpGet]
        public IActionResult Register(HttpSession session, HttpResponse response)
        {
            if (AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/topics");
                return null;
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model, HttpResponse response)
        {
            if (!this.service.IsRegisterModelValid(model))
            {
                this.Redirect(response, "/forum/register");
                return null;
            }

            User user = this.service.GetUserFromRegisterBind(model);
            this.service.RegisterUser(user);
            this.Redirect(response, "/forum/login");
            return null;
        }

        [HttpGet]
        public IActionResult Login(HttpSession session, HttpResponse response)
        {
            if (AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/topics");
                return null;
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginBindingModel model, HttpResponse response, HttpSession session)
        {
            if (!this.service.IsLoginModelValid(model))
            {
                this.Redirect(response, "/forum/login");
            }

            User user = this.service.GetUserLoginRegisterBind(model);
            this.service.LoginUser(user, session.Id);
            this.Redirect(response, "/home/topics");
            return null;
        }

        [HttpGet]
        public void Logout(HttpSession session, HttpResponse response)
        {
            AuthenticationManager.Logout(session.Id, response);
            this.Redirect(response, "/home/topics");
        }

        [HttpGet]
        public IActionResult<ProfileViewModel> Profile(HttpSession session, HttpResponse response, int id)
        {
            User user = AuthenticationManager.GetAuthenticatedUser(session.Id);

            int currentUserId = -1;
            if (user != null)
            {
                currentUserId = user.Id;
            }

            ProfileViewModel topics = this.service.GetProfileVm(id, currentUserId);

            return this.View(topics);
        }
    }
}
