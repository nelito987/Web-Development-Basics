using PizzaMoreMVC.Utilities;
using SimpleMVC.Interfaces;
using System.IO;

namespace PizzaMoreMVC.Views.Users
{
    public class Signin : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText(Constants.SigninHtmlLocation);
        }
    }
}
