using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Categories
{
    public class All : IRenderable<AllViewModel>
    {
        public AllViewModel Model { get; set; }
        public string Render()
        {
            var navigationHtml = File.ReadAllText(Constants.NavLoggedHtml);
            var navigation = string.Format(navigationHtml, Model.User.Username);

            var categoriesHtml = File.ReadAllText(Constants.AdminCategoriesHtml);
            var categories = string.Format(categoriesHtml, Model);

            StringBuilder sb = new StringBuilder();
            sb.Append(File.ReadAllText(Constants.HeaderHtml));
            sb.Append(navigation);
            sb.Append(categories);
            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
