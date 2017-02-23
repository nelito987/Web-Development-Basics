using System;
using SimpleMVC.Interfaces;
using System.IO;

namespace SharpStore.Views.Home
{
    class About : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/about.html");
        }
    }
}
