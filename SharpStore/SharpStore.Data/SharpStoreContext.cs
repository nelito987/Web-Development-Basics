namespace SharpStore.Data
{
    using Models;
    using System.Data.Entity;
    using System.Linq;

    public class SharpStoreContext : DbContext
    {        
        public SharpStoreContext()
            : base("SharpStoreContext")
        {
        }
        public virtual DbSet<Knive> Knives { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}