using System.Collections.Generic;
using PizzaForumApp.Services;
using PizzaForumApp.ViewModels;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Controllers
{
    public class HomeController: Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        [HttpGet]
        public IActionResult<IEnumerable<TopicsViewModel>>  Topics()
        {
            IEnumerable<TopicsViewModel> topics = this.service.GetTopTenTopics();
            return this.View(topics);
        }
    }
}
