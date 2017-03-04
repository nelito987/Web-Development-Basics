using System.IO;
using System.Text;
using IssueTracker.Models.ViewModels;
using IssueTracker.Utilities;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Views.Issues
{
    public class Edit : IRenderable<EditIssueVM>
    {
        public string Render()
        {
            string header = File.ReadAllText(Constants.HeaderHtml);
            string menu = string.Format(File.ReadAllText(Constants.MenuLoggedHtml), Model.LoggedInUserViewModel.Username);
            //string editIssue = File.ReadAllText(Constants.IssueEditHtml);
            string editIssue = File.ReadAllText("../../content/issue-edit.html").Replace("##id##", this.Model.Id.ToString());
            string footer = File.ReadAllText(Constants.FooterHtml);

            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            sb.Append(menu);
            sb.Append(editIssue);
            sb.Append(footer);

            return sb.ToString();
        }

        public EditIssueVM Model { get; set; }
    }
}
