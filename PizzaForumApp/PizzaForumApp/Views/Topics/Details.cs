using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Topics
{
    public class Details: IRenderable<DetailsViewModel>
    {
        public DetailsViewModel Model { get; set; }
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(File.ReadAllText(Constants.HeaderHtml));
            sb.Append(File.ReadAllText(Constants.NavLoggedHtml));
           
            sb.Append("<div class=\"container\">");
            sb.Append(this.Model.Topic);
            foreach (var reply in this.Model.Replies)
            {
                sb.Append(reply);
            }

            string form = File.ReadAllText(Constants.ReplyForm);
            form = string.Format(form, this.Model.Topic.Title);
            sb.Append(form);
            sb.Append("</div>");

            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
