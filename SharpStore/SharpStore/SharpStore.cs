using SimpleHttpServer;
using SimpleHttpServer.Models;
using System.Collections.Generic;
using System.IO;

namespace SharpStore
{
    class SharpStore
    {
        static void Main(string[] args)
        {
            var routes = RoutesConfig.GetRoutes();                
            HttpServer httpServer = new HttpServer(8081, routes);
            httpServer.Listen();
        }
    }
}