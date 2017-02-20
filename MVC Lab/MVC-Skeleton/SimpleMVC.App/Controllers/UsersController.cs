﻿using SimpleMVC.App.BindingModels;
using SimpleMVC.App.Data;
using SimpleMVC.App.Models;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMVC.App.Controllers
{
    public class UsersController: Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };

            using (var context = new NotesAppContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View();
        }

        //[HttpPost]
        //public IActionResult<UserViewModel> Register(RegisterUserBindingModel model)
        //{
        //    var user = new User()
        //    {
        //        Username = model.Username,
        //        Password = model.Password
        //    };

        //    using(var context = new NotesAppContext())
        //    {
        //        context.Users.Add(user);
        //        context.SaveChanges();
        //    }
        //    var viewModel = new UserViewModel()
        //    {
        //        Username = model.Username
        //    };           
            
        //    return View(viewModel);
        //}

            [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            List<string> usernames = null;
            using(var context = new NotesAppContext())
            {
                usernames = context.Users.Select(u => u.Username).ToList();
            }

            var viewModel = new AllUsernamesViewModel()
            {
                Usernames = usernames
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {            
            using (var context = new NotesAppContext())
            {
                var user = context.Users.Find(id);
                var viewModel = new UserProfileViewModel()
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Notes = user.Notes
                    .Select(x => new NoteViewModel()
                    {
                        Title = x.Title,
                        Content = x.Content
                    })
                };
                return View(viewModel);
            }    
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            using (var context = new NotesAppContext())
            {
                var user = context.Users.Find(model.UserId);
                var note = new Note()
                {
                    Title = model.Title,
                    Content = model.Content
                };
                user.Notes.Add(note);
                context.SaveChanges();
            }
            return Profile(model.UserId);
        }
    }
}