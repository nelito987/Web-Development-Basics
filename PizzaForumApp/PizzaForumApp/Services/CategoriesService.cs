﻿using System.Collections.Generic;
using System.Linq;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;
using PizzaForumApp.ViewModels;

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
    }
}