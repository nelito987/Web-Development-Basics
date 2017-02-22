using SimpleHttpServer.Models;
using SimpleMVC.App.MVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.MVC.Security
{
    public class SignInManager
    {
        private IDbIdentityContext dbContext;
        public SignInManager(IDbIdentityContext context)
        {
            this.dbContext = context;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            if(session == null)
            {
                return false;
            }
            var currentSession = this.dbContext.Logins.FirstOrDefault(s => s.SessionId == session.Id && s.IsActive == true);
            if (currentSession == null)
            {
                return false;
            }
            return true;
        }

        internal void Logout(HttpSession session)
        {
            var login = dbContext.Logins.FirstOrDefault(s => s.SessionId == session.Id && s.IsActive == true);
            login.IsActive = false;
            dbContext.SaveChanges();
        }
    }
}
