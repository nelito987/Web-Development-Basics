using PizzaMore.Data;
using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMore.SignIn
{
    public class SignIn
    {
        public static IDictionary<string, string> RequestParameters;
        public static Header Header = new Header();
        static void Main(string[] args)
        {
            if (WebUtil.IsGet())
            {
                Header.Print();
                WebUtil.PrintFileContent("views/signin.html");
            }
            else if (WebUtil.IsPost())
            {
                LogIn();
                Header.Print();
                WebUtil.PrintFileContent("views/signin.html");
            }
        }

        private static void LogIn()
        {

            RequestParameters = WebUtil.RetrievePostParameters();
            string userEmail = RequestParameters["email"];
            string password = RequestParameters["password"];
            string hashedPass = PasswordHasher.Hash(password);

            using (var ctx = new PizzaMoreContext())
            {
                var user = ctx.Users.SingleOrDefault(u => u.Email == userEmail);
                if (hashedPass == user.Password)
                {
                    var session = new Session()
                    {
                        Id = new Random().Next().ToString(),
                        User = user
                    };

                    if (user != null)
                    {
                        Header.AddCookie(new Cookie("sid", session.Id));
                    }
                    ctx.Sessions.Add(session);
                    ctx.SaveChanges();
                }

            }
        }
    }
}
