using PizzaMoreMVC.Data;

namespace PizzaMoreMVC.Services
{
    public abstract class Service
    {
        protected PizzaMvcContext context;

        public Service(PizzaMvcContext context)
        {
            this.context = context;
        }
    }
}
