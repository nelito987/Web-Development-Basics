namespace PizzaMoreMVC.Data
{
    public class Data
    {
        private static PizzaMvcContext context;

        public static PizzaMvcContext Context
        {
            get
            {
                return context ?? (context = new PizzaMvcContext());
            }
        }
    }
}
