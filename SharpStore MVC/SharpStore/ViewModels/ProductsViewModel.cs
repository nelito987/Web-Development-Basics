using System;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace SharpStore.ViewModels
{
    public class ProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public override string ToString()
        {
            var template = "<div class=\"col-sm-4\">" +
                    "<div class=\"thumbnail\">" +
                  $"<img src = \"{this.ImageUrl}\">" +
                  $"<div class=\"caption\"><h3>{this.Name}</h3><p>{this.Price}</p><p><a href = \"{this.ImageUrl}\" class=\"btn btn-primary\" role=\"button\">Buy Now</a></p></div></div></div>";
            return template;
        }
        
    }
}
