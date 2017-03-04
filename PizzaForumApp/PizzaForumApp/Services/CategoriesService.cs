using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;
using PizzaForumApp.ViewModels;
using SimpleHttpServer.Models;

namespace PizzaForumApp.Services
{
    public class CategoriesService: Service
    {
         
        public AllViewModel GetAllViewModel(User activeUser)
        {
            AllViewModel view = new AllViewModel();
            LoggedInUserViewModel loggedIn = new LoggedInUserViewModel()
            {
                Username = activeUser.Username
            };

            IEnumerable<AllCategoryViewModel> categories = this.Context
                .Categories
                .Select(category => new AllCategoryViewModel()
                {
                    Id = category.Id,
                    CategoryName = category.Name
                });
            view.User = loggedIn;
            view.Categories = categories;
            return view;
        }

        public bool IsNewCategoryValid(NewCategoryBindingModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return false;
            }
            return true;
        }

        public void AddNewCategory(NewCategoryBindingModel model)
        {
            this.Context.Categories.Add(new Category()
            {
                Name = model.Name
            });

            Context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            Category category = this.Context.Categories.Find(id);
            this.Context.Categories.Remove(category);
            this.Context.SaveChanges();
        }

        public EditCategoryViewModel GetEditCategoryModel(int id)
        {
            return new EditCategoryViewModel()
            {
                CategoryName = Data.Data.Context.Categories.Find(id).Name,
                Id = id
            };
        }

        public void EditCategory(EditBindingModel model)
        {
            Category category = Data.Data.Context.Categories.Find(model.CategoryId);
            if (category != null)
            {
                category.Name = model.CategoryName;
                Data.Data.Context.SaveChanges();
            }
        }

        public IEnumerable<TopicsViewModel> GetCategoryTopicsByName(string categoryName)
        {
            var topicVm = Context.Categories
                .FirstOrDefault(c => c.Name == categoryName).Topics.Select(tp => new TopicsViewModel
                {
                    Id= tp.Id,
                    AuthorUsername = tp.Author.Username,
                    CategoryName = tp.Category.Name,
                    PublishDate = tp.PublishDate,
                    RepliesCount = tp.Replies.Count
                });

            return topicVm;

        }
    }
}
