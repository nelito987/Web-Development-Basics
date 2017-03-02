using System.Linq;
using System.Text.RegularExpressions;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;

namespace PizzaForumApp.Services
{
    public class ForumService: Service
    {
        public bool IsRegisterModelValid(RegisterUserBindingModel model)
        {
            if (model.Username.Length < 3)
            {
                return false;
            }

            Regex regex = new Regex("[a-z0-9]+$");
            if (!regex.IsMatch(model.Username))
            {
                return false;
            }

            if (!model.Email.Contains("@"))
            {
                return false;
            }

            Regex passRegex = new Regex("[0-9]{4,}");
            var passCheck = !passRegex.IsMatch(model.Password);
            var passCheck2 = model.Password != model.ConfirmPassword;
            if (passCheck || passCheck2)
            {
                return false;
            }

            if (this.Context.Users.Any(u => u.Username == model.Username ||
                                            u.Email == model.Email))
            {
                return false;
            }

            return true;
        }

        public User GetUserFromRegisterBind(RegisterUserBindingModel model)
        {
            User user = new User()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email
            };
            return user;
        }

        public void RegisterUser(User user)
        {
            if (!this.Context.Users.Any())
            {
                user.IsAdministrator = true;
            }

            this.Context.Users.Add(user);
            this.Context.SaveChanges();
        }

        public bool IsLoginModelValid(LoginBindingModel model)
        {
            return this.Context.Users.Any(u => (u.Email == model.Credential || u.Username == model.Credential)
                                               && u.Password == model.Password);
        }

        public User GetUserLoginRegisterBind(LoginBindingModel model)
        {
            return this.Context.Users.FirstOrDefault(u => (u.Email == model.Credential || u.Username == model.Credential)
                                                && u.Password == model.Password);
        }

        public void LoginUser(User user, string sessionId)
        {
            this.Context.Logins.Add(new Login()
            {
                User = user,
                SessionId = sessionId,
                IsActive = true
            });
            this.Context.SaveChanges();
        }
    }
}
