using System.IO;
using System.Text;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Views.Forum
{
    public class Profile : IRenderable<ProfileViewModel>
    {
        public ProfileViewModel Model { get; set; }
        public string Render()
        {
            
            {
                string header = File.ReadAllText(Constants.HeaderHtml);

                string navigation;
                string currentUser = ViewBag.GetUserName();
                if (currentUser != null)
                {
                    navigation = File.ReadAllText(Constants.NavLoggedHtml);
                    navigation = string.Format(navigation, currentUser);
                }
                else
                {
                    navigation = File.ReadAllText(Constants.NavNotLoggedHtml);
                }

                string profile = File.ReadAllText(Constants.ProfileMine);


                StringBuilder topics = new StringBuilder();
                foreach (var topic in this.Model.Topics)
                {
                    topics.Append("<tr>");
                    topics.Append(topic.ToString());
                    if (this.Model.ClickeUserId == this.Model.CurrentUserId)
                    {
                        topics.Append(topic.GetDeleteButton());
                    }

                    topics.Append("</tr>");
                }

                profile = string.Format(profile, this.Model.ClickedUsername, topics);
                string footer = File.ReadAllText(Constants.FooterHtml);

                StringBuilder builder = new StringBuilder();
                builder.Append(header);
                builder.Append(navigation);
                builder.Append(profile);
                builder.Append(footer);

                return builder.ToString();
            }
        }
    }
}
