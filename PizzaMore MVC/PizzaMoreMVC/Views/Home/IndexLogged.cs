using PizzaMoreMVC.Utilities;
using SimpleMVC.Interfaces;
using System.IO;

namespace PizzaMoreMVC.Views.Home
{
    public class Indexlogged : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText(Constants.LoggedHtmlLocation);
        }
    }
}
