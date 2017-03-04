using IssueTracker.Data.Services;
using IssueTracker.Models.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Controllers
{
    public class HomeController: Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        [HttpGet]
        public IActionResult<LoggedInVM> Index(HttpSession session)
        {
            return this.View(this.service.CheckedForLoggedInUser(session));
        }
    }
}
