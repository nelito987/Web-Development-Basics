using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaForumApp.ViewModels
{
    public class DetailsTopicViewModel
    {
        public string Title { get; set; }

        public string AuthorUsername { get; set; }

        public DateTime? PublishDate { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            string representation = $"<div class=\"thumbnail\">\r\n\t" +
                                    $"<h4><strong><a href=\"#\">{this.Title}</a><strong></h4>\r\n\t" +
                                    $"<p><a href=\"#\">{this.AuthorUsername}</a> {this.PublishDate}</p>\r\n\t" +
                                    $"<p>{this.Content}</p>\r\n</div>";

            return representation;
        }
    }
}
