using PizzaForumApp.Data;

namespace PizzaForumApp.Services
{

    public abstract class Service
    {
        protected Service()
        {
            this.Context = Data.Data.Context;
        }

        protected PizzaForumContext Context { get; }
    }
}
