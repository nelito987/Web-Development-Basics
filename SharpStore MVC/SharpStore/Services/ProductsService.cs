using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpStore.Data;
using SharpStore.ViewModels;
using SharpStore.Models;

namespace SharpStore.Services
{
    class ProductsService
    {
        private SharpStoreContext context;

        public ProductsService(SharpStoreContext context)
        {
            this.context = context;
        }

        internal IEnumerable<ProductsViewModel> GetProducts()
        {
            var knives = this.context.Knives.ToArray();
            List<ProductsViewModel> viewModels = new List<ProductsViewModel>();
            foreach (Knife knife in knives)
            {
                viewModels.Add(new ProductsViewModel()
                {
                    Id = knife.Id,
                    Name = knife.Name,
                    Price = knife.Price,
                    ImageUrl = knife.ImageUrl
                });
            }
            return viewModels;
        }
    }
}
