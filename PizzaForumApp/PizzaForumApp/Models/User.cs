using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaForumApp.Models
{
    public class User
    {
        public User()
        {
            this.TopicsCreated = new HashSet<Topic>();
        }
        [Key]
        public int Id { get; set; }
       
        public string Username { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsAdministrator { get; set; }

        public ICollection<Topic> TopicsCreated { get; set; }

    }
}
