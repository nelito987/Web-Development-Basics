using System.Linq;
using PizzaForumApp.Models;

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
    }
}
