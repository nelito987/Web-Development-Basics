using SimpleMVC.Interfaces;
using System.IO;

namespace SharpStore.Views.Home
{
    public class Buy : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/buy.html");
        }
    }
}
