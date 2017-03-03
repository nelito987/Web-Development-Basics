using System.Collections.Generic;
using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Home
{
public class Topics: IRenderable<IEnumerable<TopicsViewModel>>
    {
        public IEnumerable<TopicsViewModel> Model { get; set; }
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            //header
            sb.Append(File.ReadAllText(Constants.HeaderHtml));

            //navbar
            string navigation;
            string currentUser = ViewBag.GetUserName();
            if (currentUser != null)
            {
                navigation = File.ReadAllText(Constants.NavLoggedHtml);
                navigation = string.Format(navigation, currentUser);
            }
            else
            {
                navigation = File.ReadAllText(Constants.NavNotLoggedHtml);
            }
            sb.Append(navigation);

            //topics
           
            sb.Append("<div class=\"container\">");

            if (currentUser != null)
            {
                sb.Append("<a class=\"btn btn-success\" href=\"/topics/new\">New Topic</a>");
            }

            foreach (var vm in this.Model)
            {
                sb.Append(vm);
            }
            sb.Append("</div>");
            
            //footer
            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
