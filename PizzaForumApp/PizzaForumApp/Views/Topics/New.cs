using System.Collections.Generic;
using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Topics
{
    public class New: IRenderable<IEnumerable<string>>
    {
        public IEnumerable<string> Model { get; set; }
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(File.ReadAllText(Constants.HeaderHtml));
            sb.Append(File.ReadAllText(Constants.NavNotLoggedHtml));
            
            string topic = File.ReadAllText(Constants.TopicNewHtml);

            StringBuilder options = new StringBuilder();
            foreach (string category in Model)
            {
                options.Append($"<option value=\"{category}\")>{category}</option>");
            }
            sb.Append(string.Format(topic, options));

            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
