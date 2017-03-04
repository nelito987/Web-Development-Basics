using IssueTracker.Models.ViewModels;

namespace IssueTracker.Models.BindingModels
{
    public class EditIssueBM
    {
        public int IssueId { get; set; }

        public string IssueName { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }
    }
}
