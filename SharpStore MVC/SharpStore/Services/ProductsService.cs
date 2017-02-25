using System.Collections.Generic;
using System.Linq;
using SharpStore.Data;
using SharpStore.ViewModels;
using SharpStore.Models;

namespace SharpStore.Services
{
    class ProductsService: Service
    {
        public ProductsService(SharpStoreContext context)
            : base(context) { }

        internal IEnumerable<ProductsViewModel> GetProducts(string Name)
        {
            var knifes = new List<Knife>();
            if(Name == "" || string.IsNullOrEmpty(Name))
            {
                knifes = this.context.Knives.ToList();
            }
            else
            {
                knifes = this.context.Knives.Where(k => k.Name.Contains(Name)).ToList();
            }
            
            List<ProductsViewModel> viewModels = new List<ProductsViewModel>();
            foreach (Knife knife in knifes)
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
