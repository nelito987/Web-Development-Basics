using System.Linq;
using System.Text.RegularExpressions;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Models;

namespace PizzaForumApp.Services
{
    public class ForumService: Service
    {
        public bool IsViewModelValid(RegisterUserBindingModel model)
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

        public User GetUserFromBind(RegisterUserBindingModel model)
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
    }
}
