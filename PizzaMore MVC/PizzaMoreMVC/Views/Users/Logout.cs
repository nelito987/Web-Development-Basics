using System;
using SimpleMVC.Interfaces;

namespace PizzaMoreMVC.Views.Users
{
    public class Logout : IRenderable
    {
        public string Render()
        {
            return "Logout...";
        }
    }
}
