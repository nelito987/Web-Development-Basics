using PizzaMore.Data;
using PizzaMore.Utility;
using System.Collections.Generic;

namespace PizzaMore.SignUp
{
    public static class SignUp
    {
        public static IDictionary<string, string> RequestParameters;
        public static Header Header = new Header();
        static void Main(string[] args)
        {
            if (WebUtil.IsGet())
            {
                Header.Print();
                WebUtil.PrintFileContent("views/signup.html");
            }
            else if (WebUtil.IsPost())
            {
                Register();
                Header.Print();
                WebUtil.PrintFileContent("views/signup.html");
            }
        }

        private static void Register()
        {

            RequestParameters = WebUtil.RetrievePostParameters();
            string userEmail = RequestParameters["email"];
            string password = RequestParameters["password"];
            var user = new User();
            user.Email = userEmail;
            user.Password = PasswordHasher.Hash(password);

            using (var ctx = new PizzaMoreContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
    }
}
