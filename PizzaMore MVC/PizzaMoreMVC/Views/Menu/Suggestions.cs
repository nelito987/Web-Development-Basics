using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMoreMVC.Utilities;
using PizzaMoreMVC.ViewModels;
using SimpleMVC.Interfaces.Generic;

namespace PizzaMoreMVC.Views.Menu
{
    public class Suggestions : IRenderable<PizzasViewModel>
    {
        public PizzasViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder htmlContent = new StringBuilder();
            htmlContent.AppendLine(File.ReadAllText(Constants.YourSuggestionsTopFolderLocation));
            htmlContent.AppendLine("<ul>");
            foreach (var suggestion in this.Model.PizzaSuggestions)
            {
                htmlContent.AppendLine("<form method=\"POST\">");
                htmlContent.AppendLine($"<li><a href=\"DetailsPizza?pizzaid={suggestion.Id}\">{suggestion.Title}</a> <input type=\"hidden\" name=\"pizzaId\" value=\"{suggestion.Id}\"/>" +
                                       $"<button type=\"submit\" name=\"PizzaId\" value=\"{suggestion.Id}\">X</button>");
                htmlContent.AppendLine("</form>");
            }
            htmlContent.AppendLine("</ul>");

            htmlContent.AppendLine(File.ReadAllText(Constants.YourSuggestionsBottomFolderLocation));

            return htmlContent.ToString();
        }
    }
}
