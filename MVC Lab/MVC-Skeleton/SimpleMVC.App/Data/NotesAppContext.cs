namespace SimpleMVC.App.Data
{
    using Models;
    using MVC.Interfaces;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NotesAppContext : DbContext, IDbIdentityContext
    {        
        public NotesAppContext()
            : base("NotesAppContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Login> Logins { get; }

        void IDbIdentityContext.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}