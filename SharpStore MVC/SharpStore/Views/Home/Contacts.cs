using SimpleMVC.Interfaces;
using System.IO;

namespace SharpStore.Views.Home
{
    class Contacts: IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/contacts.html");
        }
    }
}
