using System.IO;
using System.Text;
using IssueTracker.Models.ViewModels;
using IssueTracker.Utilities;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Views.Home
{
    public class Index : IRenderable<LoggedInVM>
    {
        public LoggedInVM Model { get; set; }
        public string Render()
        {
            string header = File.ReadAllText(Constants.HeaderHtml);
            string menu = File.ReadAllText(Constants.MenuHtml);
            string menuLogged = File.ReadAllText(Constants.MenuLoggedHtml);
            string home = File.ReadAllText(Constants.HomeHtml);
            string footer = File.ReadAllText(Constants.FooterHtml);

            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            if (Model.Username != null)
            {
                sb.Append(string.Format(menuLogged, Model.Username));
            }
            else
            {
                sb.Append(menu);
            }
            
            sb.Append(home);
            sb.Append(footer);

            return sb.ToString();
        }
    }
}
