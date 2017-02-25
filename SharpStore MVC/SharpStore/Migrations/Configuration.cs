namespace SharpStore.Migrations
{
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SharpStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;      
                  
        }

        protected override void Seed(SharpStoreContext context)
        {
            context.Knives.AddOrUpdate(knives => knives.Name, new Knife[]
           {
                new Knife()
                {
                    Name = "Master Chef Type Thunderbolt",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61uxu8TpdSL.png",
                    Price = 25
                },
                new Knife()
                {
                    Name = "Master Chef Knife",
                    ImageUrl = "http://2static.fjcdn.com/pictures/Cs_86123f_5718085.jpg",
                    Price = 15
                },
                new Knife()
                {
                    Name = "Master Chef Type CSS",
                    ImageUrl = "https://steamcommunity-a.akamaihd.net/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpovbSsLQJf1fLEcjVL49KJnJm0kfjmNqjFqWle-sBwhtbM8Ij8nVn6_xJvYm7wJ4OUegFsMF_SrFK5lOrohpHutcvPnyQy6HMi4SmOzkCyhAYMMLLPBEUFzQ/256fx255f",
                    Price = 35
                },
                new Knife()
                {
                    Name = "Master Chef Type C",
                    ImageUrl = "http://csgoclick.com/items/1.png",
                    Price = 20
                },
           });
        }
    }
}
