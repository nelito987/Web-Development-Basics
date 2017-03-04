using System.IO;
using System.Text;
using IssueTracker.Utilities;
using SimpleMVC.Interfaces;

namespace IssueTracker.Views.Issues
{
    public class New : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText(Constants.HeaderHtml);
            string menuLogged = string.Format(File.ReadAllText(Constants.MenuLoggedHtml));
            //string menu = File.ReadAllText(Constants.MenuHtml);
            string newIssue = File.ReadAllText(Constants.IssueNewHtml);
            string footer = File.ReadAllText(Constants.FooterHtml);

            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            sb.Append(menuLogged);
            sb.Append(newIssue);
            sb.Append(footer);

            return sb.ToString();
        }
    }
}
