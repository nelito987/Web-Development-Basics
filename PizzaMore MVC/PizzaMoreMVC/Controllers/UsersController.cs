using PizzaMoreMVC.BindingModels;
using PizzaMoreMVC.Models;
using PizzaMoreMVC.Services;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using System;
using System.Linq;

namespace PizzaMoreMVC.Controllers
{
    public class UsersController: Controller
    {
        [HttpGet]
        public IActionResult Signup()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Signup(SignupBindingModel model)
        {            
            UsersService service = new UsersService(Data.Data.Context);
            service.AddNewUser(model);
            return this.View("Home", "Index");
            //Redirect(new HttpResponse(){ }, "/home/index");
            //return null;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Signin(SigninBindingModel model, HttpSession session)
        {
            UsersService service = new UsersService(Data.Data.Context);
            bool signInSucceed = service.SignInUser(model, session);

            if (signInSucceed)
            {
                return this.View("Home", "Indexlogged");
            }
            //Console.WriteLine("<h1>Invalid username or password!!!</h>");
            return this.View();           
        }
    }
}
