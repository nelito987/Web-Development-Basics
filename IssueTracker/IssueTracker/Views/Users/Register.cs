using System.Collections.Generic;
using System.IO;
using System.Text;
using IssueTracker.Models.ViewModels;
using IssueTracker.Utilities;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Views.Users
{
    public class Register : IRenderable<HashSet<RegistrationErrorVM>>
    {
        public HashSet<RegistrationErrorVM> Model { get; set; }
        public string Render()
        {
            string header = File.ReadAllText(Constants.HeaderHtml);
            string menu = File.ReadAllText(Constants.MenuHtml);
            string register = File.ReadAllText(Constants.RegisterHtml);
            string footer = File.ReadAllText(Constants.FooterHtml);

            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            sb.Append(menu);
            StringBuilder errorBuilder = new StringBuilder();
            foreach (var eror in Model)
            {
                errorBuilder.Append(eror.ToString());
            }
            sb.Append(errorBuilder);
            sb.Append(register);
            sb.Append(footer);

            return sb.ToString();
        }
    }
}
