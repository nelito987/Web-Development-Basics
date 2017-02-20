using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.Models
{
    public class User
    {
        public User()
        {
            this.Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

    }
}
