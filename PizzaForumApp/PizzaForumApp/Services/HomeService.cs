using System.Collections.Generic;
using System.Linq;
using PizzaForumApp.ViewModels;
using PizzaForumApp.Views.Home;

namespace PizzaForumApp.Services
{
    public class HomeService: Service
    {
        public IEnumerable<TopicsViewModel> GetTopTenTopics()
        {
            IEnumerable<TopicsViewModel> result = Data.Data.Context.Topics
                .OrderByDescending(t => t.PublishDate)
                .Take(10)
                .Select(t => new TopicsViewModel
                {
                    CategoryName = t.Category.Name,
                    AuthorUsername = t.Author.Username,
                    PublishDate = t.PublishDate,
                    RepliesCount = t.Replies.Count,
                    Title = t.Title,
                    Id = t.Id
                });
            return result;
        }
    }
}
