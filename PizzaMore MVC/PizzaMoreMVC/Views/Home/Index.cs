using PizzaMoreMVC.Utilities;
using SimpleMVC.Interfaces;
using System;
using System.IO;

namespace PizzaMoreMVC.Views.Home
{
    public class Index : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText(Constants.HomeHtmlLocation);
        }
    }
}
