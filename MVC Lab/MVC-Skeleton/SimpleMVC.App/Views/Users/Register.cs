using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.Views.Users
{
    public class Register : IRenderable//<UserViewModel>
    {
        //public UserViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h1>Register new user</h1><br/>");
            sb.AppendLine("<a href=\"/home/index\">Home<a/>");
            sb.AppendLine("<form action=\"register\" method=\"post\"><br/>");
            sb.AppendLine("Username: <input type=\"text\" name=\"Username\"><br/>");
            sb.AppendLine("Password: <input type=\"password\" name=\"Password\"><br/>");
            sb.AppendLine("<input type=\"submit\" value=\"Submit\">");
            sb.AppendLine("</form><br/>");
            return sb.ToString();
        }
    }
}
