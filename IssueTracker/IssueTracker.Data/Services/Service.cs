using System.Linq;
using IssueTracker.Models.DataModels;
using IssueTracker.Models.ViewModels;
using SimpleHttpServer.Models;

namespace IssueTracker.Data.Services
{
    public abstract class Service
    {
        protected Service()
        {
            this.Context = Data.Context;
        }

        protected IssueTrackerContext Context { get; }

        public User FindUserBySession(HttpSession session)
        {
            return Data.Context.Logins.FirstOrDefault(l => l.SessionId == session.Id).User;
        }

        public LoggedInVM CheckedForLoggedInUser(HttpSession session)
        {
            var login = Data.Context.Logins.FirstOrDefault(l => l.SessionId == session.Id && l.IsActive);
            if (login != null)
            {
                LoggedInVM liuvm = new LoggedInVM()
                {
                    Username = login.User.Username
                };
                return liuvm;
            }
            else
            {
                return new LoggedInVM();
            }
        }
    }
}
