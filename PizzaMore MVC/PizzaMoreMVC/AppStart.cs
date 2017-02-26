using SimpleHttpServer;
using SimpleMVC;

namespace PizzaMoreMVC
{
    class AppStart
    {
        static void Main(string[] args)
        {

            HttpServer server = new HttpServer(8081, RouteTable.Routes);
            MvcEngine.Run(server, "PizzaMoreMVC");
        }
    }
}
