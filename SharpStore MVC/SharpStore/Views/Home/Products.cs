﻿using SimpleMVC.Interfaces;
using System.IO;

namespace SharpStore.Views.Home
{
    class Products: IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../content/products.html");
        }
    }
}
