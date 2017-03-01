using PizzaMoreMVC.Utilities;
using PizzaMoreMVC.ViewModels;
using SimpleMVC.Interfaces.Generic;
using System;
using System.IO;
using System.Text;

namespace PizzaMoreMVC.Views.Menu
{
    public class Index : IRenderable<PizzaSuggestionModel>
    {
        public PizzaSuggestionModel Model { get; set; }

        public string Render()
        {
            StringBuilder htmlContent = new StringBuilder();
            htmlContent.AppendLine("<nav class=\"navbar navbar-default\">" +
                "<div class=\"container-fluid\">" +
                "<div class=\"navbar-header\">" +
                "<a class=\"navbar-brand\" href=\"Home.exe\">PizzaMore</a>" +
                "</div>" +
                "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">" +
                "<ul class=\"nav navbar-nav\">" +
                "<li ><a href=\"/menu/add\">Suggest Pizza</a></li>" +
                "<li><a href=\"/menu/suggestions\">Your Suggestions</a></li>" +
                "</ul>" +
                "<ul class=\"nav navbar-nav navbar-right\">" +
                "<p class=\"navbar-text navbar-right\"></p>" +
                "<p class=\"navbar-text navbar-right\"><a href=\"/users/logout\" class=\"navbar-link\">Sign Out</a></p>" +
                $"<p class=\"navbar-text navbar-right\">Signed in as {Model.Email}</p>" +
                "</ul> </div></div></nav>");

            htmlContent.AppendLine(File.ReadAllText(Constants.MenuTopFolderLocation));
            htmlContent.AppendLine("<div class=\"card-deck\">");
            foreach (var pizza in this.Model.PizzaSuggestions)
            {
                htmlContent.AppendLine("<div class=\"card\">");
                htmlContent.AppendLine($"<img class=\"card-img-top\" src=\"{pizza.ImageUrl}\" width=\"200px\"alt=\"Card image cap\">");
                htmlContent.AppendLine("<div class=\"card-block\">");
                htmlContent.AppendLine($"<h4 class=\"card-title\">{pizza.Title}</h4>");
                htmlContent.AppendLine($"<p class=\"card-text\"><a href=\"/menu/DetailsPizza?pizzaId={pizza.Id}\">Recipe</a></p>");
                htmlContent.AppendLine("<form method=\"POST\">");
                htmlContent.AppendLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"PizzaVote\" value=\"Up\">Up</label></div>");
                htmlContent.AppendLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"PizzaVote\" value=\"Down\">Down</label></div>");
                htmlContent.AppendLine($"<input type=\"hidden\" name=\"PizzaId\" value=\"{pizza.Id}\" />");
                htmlContent.AppendLine("<input type=\"submit\" class=\"btn btn-primary\" value=\"Vote\" />");
                htmlContent.AppendLine("</form>");
                htmlContent.AppendLine("</div>");
                htmlContent.AppendLine("</div>");
            }
            htmlContent.AppendLine("</div>");

            htmlContent.AppendLine(File.ReadAllText(Constants.MenuBottomFolderLocation));

            return htmlContent.ToString();
        }
    }
}
