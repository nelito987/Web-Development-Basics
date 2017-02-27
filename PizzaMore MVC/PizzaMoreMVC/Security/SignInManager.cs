using PizzaMoreMVC.Data;
using PizzaMoreMVC.Models;
using SimpleHttpServer.Models;
using System;
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

        public void Logout(HttpSession session)
        {
            Session sessionEntity = this.dbContext
                .Sessions
                .FirstOrDefault(s => s.SessionId == session.Id);
            sessionEntity.IsActive = false;
            session.Id = new Random().Next().ToString();            
            this.dbContext.SaveChanges();
        }
    }
}
