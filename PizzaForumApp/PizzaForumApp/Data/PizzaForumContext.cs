using PizzaForumApp.Models;

namespace PizzaForumApp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PizzaForumContext : DbContext
    {
        public PizzaForumContext()
            : base("PizzaForumContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}