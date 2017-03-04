using System.Collections.Generic;
using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Home
{
    public class Categories: IRenderable<IEnumerable<string>>
    {
        public IEnumerable<string> Model { get; set; }
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

            foreach (var category in this.Model)
            {
                sb.Append($"<a href=\"/categories/topics?CategoryName={category}\">{category}</a><br>");
            }
            sb.Append("</div>");

            //footer
            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
