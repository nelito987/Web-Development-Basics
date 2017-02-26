using System.ComponentModel.DataAnnotations;

namespace PizzaMoreMVC.Models
{
    public class Session
    {
        [Key]
        public string SessionId { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.SessionId}\t{this.User.Id}";
        }
    }
}
