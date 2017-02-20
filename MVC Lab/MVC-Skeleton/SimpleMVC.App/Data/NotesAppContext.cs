namespace SimpleMVC.App.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NotesAppContext : DbContext
    {        
        public NotesAppContext()
            : base("NotesAppContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
    }
}