using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IssueTracker.Models.BindingModels;
using IssueTracker.Models.DataModels;
using IssueTracker.Models.Enums;
using IssueTracker.Models.ViewModels;

namespace IssueTracker.Data.Services
{
    public class UsersService: Service
    {
        public HashSet<RegistrationErrorVM> ValidateRegisterUser(RegisterBM urbm)
        {
            HashSet<RegistrationErrorVM> revms = new HashSet<RegistrationErrorVM>();

            if (urbm.Username.Length < 5 || urbm.Username.Length > 30)
            {
                revms.Add(new RegistrationErrorVM(Constants.UserNameLengthErrorMessage));
            }

            if (urbm.Fullname.Length < 5)
            {
                revms.Add(new RegistrationErrorVM(Constants.FullNameTooShortMessage));
            }
            //password
            //Regex specialSymbolrgx = new Regex(@"[!@#$%^&*,.]");
            
            //if (urbm.Password.Length < 8 || 
            //    !urbm.Password.Any(char.IsUpper) || 
            //    !urbm.Password.Any(char.IsDigit) ||
            //    !specialSymbolrgx.IsMatch(urbm.Password))
            if(urbm.Password.Length <3)
            {
                revms.Add(new RegistrationErrorVM(Constants.PasswordIncorrectFormatMessage));
            }
            if (urbm.Password != urbm.ConfirmPassword)
            {
                revms.Add(new RegistrationErrorVM(Constants.PasswordsDoNotMatchMessage));
            }
            return revms;
        }

        public bool RegisterUser(RegisterBM model)
        {
            Role role = Data.Context.Users.Any() ? Role.Regular : Role.Administrator;
            User user = new User()
            {
                Username = model.Username,
                Fullname = model.Fullname,
                Password = model.Password,
                Role = role
            };
            try
            {
                Data.Context.Users.Add(user);
                Data.Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public bool LoginUser(string sessionId, LoginBM model)
        {
            User user = Data.Context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == u.Password);
            if (user != null)
            {
                var currentLogin = Data.Context.Logins.FirstOrDefault(l => l.SessionId == sessionId);
                if (currentLogin != null)
                {
                    currentLogin.IsActive = true;
                }
                else
                {
                    var login = new Login()
                    {
                        User = user,
                        SessionId = sessionId,
                        IsActive = true
                    };
                    Data.Context.Logins.Add(login);
                }
                Data.Context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
