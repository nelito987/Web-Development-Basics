﻿using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using SimpleMVC.Interfaces;

namespace PizzaForumApp.Views.Forum
{
    public class Login: IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(File.ReadAllText(Constants.HeaderHtml));
            sb.Append(File.ReadAllText(Constants.NavNotLoggedHtml));
            sb.Append(File.ReadAllText(Constants.LoginHtml));
            sb.Append(File.ReadAllText(Constants.FooterHtml));

            return sb.ToString();
        }
    }
}
