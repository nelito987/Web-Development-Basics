using SimpleMVC.Interfaces;
using System.IO;
using PizzaMoreMVC.Utilities;

namespace PizzaMoreMVC.Views.Users
{
    public class Signup : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText(Constants.SignupHtmlLocation);
        }
    }
}
