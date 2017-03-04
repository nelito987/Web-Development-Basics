using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using IssueTracker.Models.BindingModels;
using IssueTracker.Models.DataModels;
using IssueTracker.Models.Enums;
using IssueTracker.Models.ViewModels;
using SimpleHttpServer.Models;

namespace IssueTracker.Data.Services
{
    public class IssuesService: Service
    {
        public IEnumerable<Issue> FindIssues()
        {
            var issues = Data.Context.Issues;
            return issues;
        }

        public IEnumerable<Issue> FindIssues(string query, string status)
        {
            if (string.IsNullOrEmpty(query) && status == "All")
            {
                return this.FindIssues();
            }
            else if (string.IsNullOrEmpty(query) && status != "All")
            {
                return new HashSet<Issue>(Data.Context.Issues.Where(i => i.Status.ToString() == status));
            }
            else if (!string.IsNullOrEmpty(query) && status != "All")
            {
                return
                    new HashSet<Issue>(
                        Data.Context.Issues.Where(i => i.Name.Contains(query) || i.Status.ToString() == status));
            }
            else
            {
                return new HashSet<Issue>(Data.Context.Issues.Where(i => i.Name.Contains(query)));
            }
        }

        public IEnumerable<IssueVM> ProduceIssueList(User activeUser, IEnumerable<Issue> issues)
        {
            HashSet<IssueVM> issuesVMCollection = new HashSet<IssueVM>();
            var placeholderIvm = new IssueVM();
            placeholderIvm.LoggedInUserViewModel = new LoggedInVM()
            {
                Username = activeUser.Username,
                IsAdmin = activeUser.Role == Role.Administrator
            };

            issuesVMCollection.Add(placeholderIvm);

            //IssueVM vm = null;
            foreach (var issue in issues)
            {
                IssueVM ivm = new IssueVM()
                {
                    LoggedInUserViewModel = new LoggedInVM
                    {
                        Username = activeUser.Username,
                        IsAdmin = activeUser.Role == Role.Administrator
                    },
                    Author = issue.Author.Username,
                    CreatedOn = issue.CreatedOn.ToString(),
                    Id = issue.Id,
                    Name = issue.Name,
                    Priority = issue.Priority.ToString(),
                    Status = issue.Status.ToString()
                };
                issuesVMCollection.Add(ivm);
            }
            return issuesVMCollection;
        }

        public bool IsNewCategoryValid(NewIssueBM model)
        {
            if (string.IsNullOrEmpty(model.IssueName) || 
                string.IsNullOrEmpty(model.Priority) || 
                string.IsNullOrEmpty(model.Status))
            {
                return false;
            }
            return true;
        }

        public void AddNewIssue(NewIssueBM model, HttpSession session)
        {
            var user = this.FindUserBySession(session);
            Data.Context.Issues.Add(new Issue()
            {
                Author = user,
                CreatedOn = DateTime.Now,
                Name = model.IssueName,
                Priority = (Priority)Enum.Parse(typeof(Priority), model.Priority),
                Status = (Status)Enum.Parse(typeof(Status), model.Status)
            });
            Context.SaveChanges();
        }

        public EditIssueVM GetEditIssueVM(User activeUser, int id)
        {
            var issue = Data.Context.Issues.Find(id);
            EditIssueVM issueVm = new EditIssueVM()
            {
                LoggedInUserViewModel = new LoggedInVM
                {
                    Username = activeUser.Username,
                    IsAdmin = activeUser.Role == Role.Administrator
                },
                Id = issue.Id
            };
            return issueVm;
        }

        public void UpdateIssue(HttpSession session, EditIssueBM model)
        {
            var user = this.FindUserBySession(session);
            var issue = Data.Context.Issues.Find(model.IssueId);
            issue.Priority = (Priority)Enum.Parse(typeof(Priority), model.Priority);
            issue.Status = (Status)Enum.Parse(typeof(Status), model.Status);
            issue.Name = model.IssueName;
            Data.Context.Issues.AddOrUpdate(issue);
            Data.Context.SaveChanges();
        }

        public void DeleteIssue(int id)
        {
            Issue category = Data.Context.Issues.Find(id);
            Data.Context.Issues.Remove(category);
            Data.Context.SaveChanges();
        }
    }
}
