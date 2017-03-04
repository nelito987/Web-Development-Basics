using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IssueTracker.Models.Enums;

namespace IssueTracker.Models.DataModels
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(30)]
        [MinLength(5)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [MinLength(5)]
        public string Fullname { get; set; }
        

        //[RegularExpression(@"^\d{4}$")]
        [Column(TypeName = "char")]
        [MinLength(8)]
        public string Password { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }

    }
}
