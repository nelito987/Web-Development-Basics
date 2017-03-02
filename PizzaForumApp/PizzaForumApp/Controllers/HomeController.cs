using PizzaForumApp.Services;
using SimpleMVC.Controllers;

namespace PizzaForumApp.Controllers
{
    public class HomeController: Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }
    }
}
