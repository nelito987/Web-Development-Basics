using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IssueTracker.Models.ViewModels;
using IssueTracker.Utilities;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Views.Issues
{
    public class All: IRenderable<IEnumerable<IssueVM>>
    {
        public IEnumerable<IssueVM> Model { get; set; }
        public string Render()
        {
            string header = File.ReadAllText(Constants.HeaderHtml);
            string menuLogged = File.ReadAllText(Constants.MenuLoggedHtml);
            string issues = File.ReadAllText(Constants.IssuesHtml);
            string footer = File.ReadAllText(Constants.FooterHtml);

            StringBuilder sb = new StringBuilder();
            //header
            sb.Append(header);
            //menu
            var user = this.Model.First().LoggedInUserViewModel.Username;
            sb.Append(string.Format(menuLogged, user));
            //issues
            StringBuilder coreBuilder = new StringBuilder();
            foreach (var issueVM in this.Model.Skip(1))
            {
                coreBuilder.Append(issueVM);
            }
            sb.Append(string.Format(issues, coreBuilder.ToString()));
            //footer
            sb.Append(footer);

            return sb.ToString();
        }
    }
}
