using PizzaMoreMVC.Security;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;

namespace PizzaMoreMVC.Controllers
{
    public class HomeController: Controller
    {
        private SignInManager signInManager;

        public HomeController()
        {
            this.signInManager = new SignInManager(Data.Data.Context);
        }

        [HttpGet]
        public IActionResult Index(HttpSession session)
        {
            if (this.signInManager.IsAuthenticated(session))
            {
                //Redirect(new HttpResponse() { }, "/home/indexlogged");
                //return null;
                return this.View("Home", "Indexlogged");
            }
            else
            {
                return this.View();
            }            
        }

        [HttpGet]
        public IActionResult Indexlogged(HttpSession session)
        {
            return this.View();
        }
    }
}
