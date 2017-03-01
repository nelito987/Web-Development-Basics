using PizzaMoreMVC.Utilities;
using SimpleMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMoreMVC.Views.Menu
{
    public class Add : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText(Constants.AddHtmlLocation);
        }
    }
}
