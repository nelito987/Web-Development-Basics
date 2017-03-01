using PizzaMoreMVC.Data;
using PizzaMoreMVC.Models;
using PizzaMoreMVC.BindingModels;
using AutoMapper;
using System.Linq;
using SimpleHttpServer.Models;
using System;
using PizzaMoreMVC.Security;

namespace PizzaMoreMVC.Services
{
    public class UsersService : Service
    {
        public UsersService(PizzaMvcContext context) : base(context)
        {
        }

        public void AddNewUser(SignupBindingModel model)
        {
            User user = new User()
            {
                Email = model.SignUpEmail,
                Password = model.SignUpPassword
            };
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool SignInUser(SigninBindingModel model, HttpSession session)
        {
            //Mapper.Initialize(e => e.CreateMap<SigninBindingModel, User>());
            User currentUser = context.Users
                .FirstOrDefault(u => u.Email == model.SignInEmail &&
                u.Password == model.SignInPassword);
            
            if (currentUser != null)
            {
                var currentsession = this.context.Sessions.Find(session.Id);
                if (currentsession == null)
                {
                    Session sessionEntity = new Session()
                    {
                        SessionId = session.Id,
                        IsActive = true,
                        User = currentUser
                    };
                    context.Sessions.Add(sessionEntity);

                    currentsession = sessionEntity;
                }

                if (currentsession.IsActive == false && currentsession.UserId == currentUser.Id)
                {
                    currentsession.IsActive = true;
                }

                context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
