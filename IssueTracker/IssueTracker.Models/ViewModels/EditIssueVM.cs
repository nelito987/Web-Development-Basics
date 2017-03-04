using System.IO;

namespace IssueTracker.Models.ViewModels
{
    public class EditIssueVM
    {
        public LoggedInVM LoggedInUserViewModel { get; set; }

        public int Id { get; set; }

        public override string ToString()
        {
            string htmlEdit = File.ReadAllText("../../Content/issue-edit.html");
            var stringa = string.Format(htmlEdit, Id);
            return stringa;
        }
    }
}
