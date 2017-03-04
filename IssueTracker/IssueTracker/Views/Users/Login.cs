using System.IO;
using System.Text;
using IssueTracker.Utilities;
using SimpleMVC.Interfaces;

namespace IssueTracker.Views.Users
{
    public class Login: IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText(Constants.HeaderHtml);
            string menu = File.ReadAllText(Constants.MenuHtml);
            string login = File.ReadAllText(Constants.LoginHtml);
            string footer = File.ReadAllText(Constants.FooterHtml);

            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            sb.Append(menu);
            sb.Append(login);
            sb.Append(footer);

            return sb.ToString();
        }
    }
}
