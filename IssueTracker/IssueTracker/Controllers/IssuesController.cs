using System.Collections;
using System.Net;
using IssueTracker.Models.DataModels;
using IssueTracker.Models.Enums;
using IssueTracker.Utilities.Security;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Data.Services;
using IssueTracker.Models.BindingModels;
using IssueTracker.Models.ViewModels;
using SimpleMVC.Interfaces.Generic;

namespace IssueTracker.Controllers
{
    public class IssuesController: Controller
    {
        private IssuesService service;

        public IssuesController()
        {
            this.service = new IssuesService();
        }
        [HttpGet]
        public IActionResult<IEnumerable<IssueVM>> All(
            HttpSession session, 
            HttpResponse response,
            string query,
            string status)
        {
            if (!SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");
                return null;
            }

            User activeUser = SignInManager.GetAuthenticatedUser(session.Id);
            IEnumerable<Issue> issues = new HashSet<Issue>();

            if (string.IsNullOrEmpty(query) && string.IsNullOrEmpty(status))
            {
                issues = this.service.FindIssues();
            }
            else
            {
                issues = this.service.FindIssues(query, status);
            }
            IEnumerable<IssueVM> issuesViewModel = this.service.ProduceIssueList(activeUser, issues);
            return this.View(issuesViewModel);
        }

        [HttpGet]
        public IActionResult New(HttpSession session,HttpResponse response)
        {
            if (!SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/login");
                return null;
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult New(HttpSession session, HttpResponse response, NewIssueBM model)
        {
            if (!SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            if (!this.service.IsNewCategoryValid(model))
            {
                this.Redirect(response, "/issues/new");
                return null;
            }

            this.service.AddNewIssue(model, session);
            this.Redirect(response, "/issues/all");
            return null;
        }

        [HttpGet]
        public IActionResult<EditIssueVM> Edit(HttpSession session, HttpResponse response, int id)
        {
            if (!SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");
                return null;
            }
            User user = SignInManager.GetAuthenticatedUser(session.Id);
            if (user.Issues.All(i => i.Id != id) && user.Role != Role.Administrator)
            {
                this.Redirect(response, "/issues/all");
                return null;
            }
            var loggedUserViewModel = this.service.CheckedForLoggedInUser(session);
            var editIssueViewModel = new EditIssueVM()
            {
                Id = id,
                LoggedInUserViewModel = loggedUserViewModel
            };
            return this.View(editIssueViewModel);
        }

        [HttpPost]
        public IActionResult Edit(HttpSession session, HttpResponse response, EditIssueBM eibm)
        {
            this.service.UpdateIssue(session, eibm);
            this.Redirect(response, "/issues/all");
            return null;
        }

        [HttpGet]
        public IActionResult Delete(HttpSession session, HttpResponse response, int id)
        {
            if (!SignInManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");
                return null;
            }

            User activeUser = SignInManager.GetAuthenticatedUser(session.Id);
            if (activeUser.Role != Role.Administrator)
            {
                this.Redirect(response, "/issues/all");
                return null;
            }

            this.service.DeleteIssue(id);
            this.Redirect(response, "issues/all");
            return null;
        }
    }
}
