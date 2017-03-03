using System;
using System.Collections.Generic;
using System.Linq;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;
using PizzaForumApp.ViewModels;

namespace PizzaForumApp.Services
{
    public class TopicsService: Service
    {
        public IEnumerable<string> GetCategoryNames()
        {
            return Data.Data.Context.Categories.Select(c => c.Name);
        }

        public void AddNewTopic(TopicBindingModel model, User user)
        {
            Category category = Context.Categories.FirstOrDefault(c => c.Name == model.Category);
            Topic newTopic = new Topic()
            {
                Author = user,
                Title = model.Title,
                Content = model.Content,
                PublishDate = DateTime.Now,
                Category = category

            };
            Context.Topics.Add(newTopic);
            Context.SaveChanges();
        }

        internal bool IsNewTopicValid(TopicBindingModel bind)
        {
            if (bind.Title.Length > 30)
            {
                return false;
            }

            if (bind.Content.Length > 5000)
            {
                return false;
            }

            return true;
        }

        public DetailsViewModel GetDetailsVm(int id)
        {
            Topic topic = Context.Topics.Find(id);
            DetailsTopicViewModel topicVm = new DetailsTopicViewModel()
            {
                Title = topic.Title,
                AuthorUsername = topic.Author.Username,
                Content = topic.Content,
                PublishDate = topic.PublishDate
            };

            IEnumerable<DetailsReplyViewModel> replies = topic
                .Replies
                .Select(r => new DetailsReplyViewModel()
                {
                    AuthorUsername = r.Author.Username,
                    Content = r.Content,
                    ImageUrl = r.ImageUrl,
                    PublishDate = r.PublishDate
                });

            DetailsViewModel model = new DetailsViewModel()
            {
                Replies = replies,
                Topic = topicVm
            };

            return model;
        }
    }
}
