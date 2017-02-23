using SimpleMVC.Interfaces;
using System.IO;

namespace SharpStore.Views.Home
{
    public class Index : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/home.html");
        }
    }
}

