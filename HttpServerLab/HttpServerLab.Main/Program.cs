using HttpServerLab.Enums;
using HttpServerLab.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerLab.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var routes = new List<Route>()
            {
                new Route
                {
                    Name = "HalloHandler",
                    UrlRegex = @"^/hello$",
                    RequestMethod = RequestMethod.Get,
                    Callable = (HttpRequest request) =>
                    {
                        return new HttpResponse()
                        {
                            ContentAsUTF8 = "<h3>Hello!!!<h3>",
                            StatusCode = ResponseStatusCode.Ok
                        };
                    }
                }
            };

            HttpServer httpServer = new HttpServer(8081, routes);
            httpServer.Listen();
        }
    }
}
