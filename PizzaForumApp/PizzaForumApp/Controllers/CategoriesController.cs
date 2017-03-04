using System.Collections.Generic;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;
using PizzaForumApp.Services;
using PizzaForumApp.Utilities;
using PizzaForumApp.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaForumApp.Controllers
{
    public class CategoriesController: Controller
    {
        private CategoriesService service;

        public CategoriesController()
        {
            this.service = new CategoriesService();
        }

        [HttpGet]
        public IActionResult<AllViewModel> All(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (!activeUser.IsAdministrator)
            {
                this.Redirect(response, "/home/topics");
                return null;
            }

            AllViewModel viewModel = this.service.GetAllViewModel(activeUser);

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult New(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (!activeUser.IsAdministrator)
            {
                this.Redirect(response, "/home/topics");
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult New(HttpSession session, HttpResponse response, NewCategoryBindingModel model)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (!activeUser.IsAdministrator)
            {
                this.Redirect(response, "/home/topics");
                return null;
            }

            if (!this.service.IsNewCategoryValid(model))
            {
                this.Redirect(response, "/categories/new");
                return null;
            }

            this.service.AddNewCategory(model);
            this.Redirect(response, "/categories/all");
            return null;
        }

        [HttpGet]
        public IActionResult Delete(HttpSession session, HttpResponse response, int id)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (!activeUser.IsAdministrator)
            {
                this.Redirect(response, "/home/topics");
                return null;
            }

            this.service.DeleteCategory(id);
            this.Redirect(response, "categories/all");
            return null;
        }

        [HttpGet]
        public IActionResult<EditCategoryViewModel> Edit(HttpSession session, HttpResponse response, int id)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (!activeUser.IsAdministrator)
            {
                this.Redirect(response, "/home/topics");
                return null;
            }

            EditCategoryViewModel viewModel = service.GetEditCategoryModel(id);
            return this.View(viewModel);

        }

        [HttpPost]
        public void Edit
              (HttpResponse response, HttpSession session, EditBindingModel bind)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/forum/login");
                
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            if (!activeUser.IsAdministrator)
            {
                this.Redirect(response, "/home/topics");
                
            }

            this.service.EditCategory(bind);
            this.Redirect(response, "/categories/all");
        }

        [HttpGet]
        public IActionResult<IEnumerable<TopicsViewModel>> Topics(
            HttpResponse response, 
            HttpSession session, 
            string categoryName)
        {
            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);
            IEnumerable<TopicsViewModel> topics = service.GetCategoryTopicsByName(categoryName);
            return this.View(topics);
        }
    }
}
