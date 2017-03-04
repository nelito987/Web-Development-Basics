namespace IssueTracker.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<IssueTracker.Data.IssueTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IssueTracker.Data.IssueTrackerContext";
        }

        protected override void Seed(IssueTracker.Data.IssueTrackerContext context)
        {
           
        }
    }
}
