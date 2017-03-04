using System;
using System.ComponentModel.DataAnnotations.Schema;
using IssueTracker.Models.Enums;

namespace IssueTracker.Models.DataModels
{
    public class Issue
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        [ForeignKey("Author")]
        public int UserId { get; set; }

        public virtual User Author { get; set; }
    }
}
