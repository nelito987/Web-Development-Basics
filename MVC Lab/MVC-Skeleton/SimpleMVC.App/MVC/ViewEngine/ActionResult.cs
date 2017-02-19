using SimpleMVC.App.MVC.Interfaces;
using System;

namespace SimpleMVC.App.MVC.ViewEngine
{
    public class ActionResult: IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            this.Action = (IRenderable)Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));
                 
        }
        public IRenderable Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}
