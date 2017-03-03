using System.Linq;
using PizzaForumApp.Models;
using SimpleHttpServer.Models;
using SimpleHttpServer.Utilities;

namespace PizzaForumApp.Utilities
{
    public class AuthenticationManager
    {
        public static bool IsAuthenticated(string sessionId)
        {
            return Data.Data.Context.Logins.Any(login => login.SessionId == sessionId && login.IsActive);
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            var user = Data.Data.Context.Logins.FirstOrDefault(l => l.SessionId == sessionId).User;
            return user;
        }

        public static void Logout(string sessionId, HttpResponse response)
        {
            Login currentLogin = Data.Data.Context.Logins.FirstOrDefault(l => l.SessionId == sessionId);
            currentLogin.IsActive = false;
            Data.Data.Context.SaveChanges();

            var session = SessionCreator.Create();
            var sessionCookie = new Cookie("sessionId", session.Id + "; HttpOnly; path=/");
            response.Header.AddCookie(sessionCookie);
        }
    }
}
