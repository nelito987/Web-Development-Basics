namespace IssueTracker.Data
{
    using Models.DataModels;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class IssueTrackerContext : DbContext
    {
        public IssueTrackerContext()
            : base("name=IssueTrackerContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }
}