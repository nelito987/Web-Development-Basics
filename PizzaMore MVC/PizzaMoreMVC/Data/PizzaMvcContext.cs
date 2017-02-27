namespace PizzaMoreMVC.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PizzaMvcContext : DbContext
    {        
        public PizzaMvcContext()
            : base("PizzaMvcContext")
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
    }    
}