using SharpStore.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

namespace SharpStore.Views.Home
{
    class Products: IRenderable<IEnumerable<ProductsViewModel>>
    {
        public IEnumerable<ProductsViewModel> Model { get; set; }

        public string Render()
        {
            var template =  File.ReadAllText("../../content/products.html");
            StringBuilder sb = new StringBuilder();
            foreach (var viewModel in Model)
            {
                sb.Append(viewModel.ToString());
            }
            return string.Format(template, sb);
        }
    }
}
