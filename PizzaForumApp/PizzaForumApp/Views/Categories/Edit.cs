using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Categories
{
    public class Edit: IRenderable<EditCategoryViewModel>
    {
        public EditCategoryViewModel Model { get; set; }
        public string Render()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(File.ReadAllText(Constants.HeaderHtml));
            sb.Append(File.ReadAllText(Constants.NavLoggedHtml));
            sb.Append(string.Format(File.ReadAllText(Constants.EditCategoriyHtml), Model));
            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
