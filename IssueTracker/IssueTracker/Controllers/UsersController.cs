using System.Collections.Generic;
using System.Linq;
using IssueTracker.Data.Services;
using IssueTracker.Models.BindingModels;
using IssueTracker.Models.ViewModels;
using IssueTracker.Utilities.Security;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Controllers
{
    public class UsersController: Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }
        [HttpGet]
        public IActionResult<HashSet<RegistrationErrorVM>> Register(HttpSession session, HttpResponse response)
        {
            if (SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/index");
                return null;
            }
            return this.View(new HashSet<RegistrationErrorVM>());
        }

        [HttpPost]
        public IActionResult<HashSet<RegistrationErrorVM>> Register(HttpSession session, HttpResponse response, RegisterBM model)
        {
            if (SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/index");
                return null;
            }
            var errors = this.service.ValidateRegisterUser(model);
            if (errors.Any())
            {
                return this.View(errors);
            }

            if (this.service.RegisterUser(model))
            {
                this.Redirect(response, "/users/login");
                return null;
            }
            this.Redirect(response, "/users/register");
            return null;
        }

        [HttpGet]
        public IActionResult Login(HttpSession session, HttpResponse response)
        {
            if (SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/index");
                return null;
            }
            return this.View();
        }
        [HttpPost]
        public IActionResult Login(HttpSession session, HttpResponse response, LoginBM model)
        {
            if (this.service.LoginUser(session.Id, model))
            {
                this.Redirect(response, "/home/index");
                return null;
            }


            return this.View();
        }

        [HttpGet]
        public IActionResult Logout(HttpSession session, HttpResponse response)
        {
            SignInManager.Logout(session.Id, response);
            this.Redirect(response, "/home/index");
            return null;
        }
    }
}
