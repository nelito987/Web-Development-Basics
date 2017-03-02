using System;
using System.Collections.Generic;

namespace PizzaForumApp.Models
{
    public class Topic
    {
        public Topic()
        {
            this.Replies = new HashSet<Reply>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        public ICollection<Reply> Replies { get; set; }
    }
}
