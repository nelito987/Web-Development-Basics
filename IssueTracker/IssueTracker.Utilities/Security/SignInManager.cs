using System.Linq;
using IssueTracker.Models.DataModels;
using SimpleHttpServer.Models;
using SimpleHttpServer.Utilities;
using static IssueTracker.Data.Data;

namespace IssueTracker.Utilities.Security
{
    public class SignInManager
    {
        public static bool IsAuthenticated(string sessionId)
        {
            return Context.Logins.Any(login => login.SessionId == sessionId && login.IsActive);
            //return Data.Data.Context.Logins.Any(l => l.IsActive && l.SessionId == session.Id);
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            var user = Context.Logins.FirstOrDefault(l => l.SessionId == sessionId).User;
            return user;
        }

        public static void Logout(string sessionId, HttpResponse response)
        {
            Login currentLogin = Context.Logins.FirstOrDefault(l => l.SessionId == sessionId);
            currentLogin.IsActive = false;
            Context.SaveChanges();

            var session = SessionCreator.Create();
            var sessionCookie = new Cookie("sessionId", session.Id + "; HttpOnly; path=/");
            response.Header.AddCookie(sessionCookie);
        }
    }
}
