using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace SimpleMVC.App.Views.Users
{
    public class All : IRenderable<AllUsernamesViewModel>
    {
        public AllUsernamesViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h3> All Users: </h3>");
            sb.AppendLine("<ul>");
            foreach (var username in Model.Usernames)
            {
                sb.AppendLine($"<li>{username}</li>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
    //public class All : IRenderable<IEnumerable<AllUsersViewModel>>
    //{
    //    public AllUsernamesViewModel Model { get; set; }
    //    IEnumerable<AllUsersViewModel> IRenderable<IEnumerable<AllUsersViewModel>>.Model { get; set; }


    //    public string Render()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.AppendLine("<h3> All Users: </h3>");
    //        sb.AppendLine("<a href=\"/home/index\">Home</a>");
    //        sb.AppendLine("<ul>");
    //        foreach (var username in ((IRenderable<IEnumerable<AllUsersViewModel>>)this).Model)
    //        {
    //            sb.AppendLine($"<li> <a href=\"/users/profile?id={username.Id}\">{username.Username}</a></li>");
    //        }
    //        sb.AppendLine("</ul>");
    //        return sb.ToString();
    //    }
    //}
}
