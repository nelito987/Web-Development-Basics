using PizzaMoreMVC.Data;
using SimpleHttpServer.Models;
using System.Linq;

namespace PizzaMoreMVC.Security
{
    public class SignInManager
    {
        private PizzaMvcContext dbContext;

        public SignInManager(PizzaMvcContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            bool isAuthenticated = this.dbContext.Sessions.Any(s => s.SessionId == session.Id && s.IsActive);
            return isAuthenticated;
        }
    }
}
