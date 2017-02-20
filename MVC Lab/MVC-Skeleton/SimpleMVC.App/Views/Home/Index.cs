using SimpleMVC.App.MVC.Interfaces;

namespace SimpleMVC.App.Views.Home
{
    public class Index: IRenderable
    {
        public string Render()
        {
            return "<h3>Hello MVC!!!</>";
        }
    }
}
