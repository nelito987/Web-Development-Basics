namespace SharpStore.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SharpStoreContext : DbContext
    {        
        public SharpStoreContext()
            : base("SharpStoreContext")
        {
            
        }

        public IDbSet<Knife> Knives { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Sale> Sales { get; set; }
    }
}