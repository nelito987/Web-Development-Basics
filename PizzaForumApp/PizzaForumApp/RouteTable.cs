using System.Collections.Generic;
using System.IO;
using SimpleHttpServer.Enums;
using SimpleHttpServer.Models;
using SimpleMVC.Routers;

namespace PizzaForumApp
{
    public class RouteTable
    {
        public static IEnumerable<Route> Routes
        {
            get
            {
                return new Route[]
                {
                        new Route()
                        {
                            Name = "Carousel CSS",
                            Method = RequestMethod.GET,
                            UrlRegex = "/css/carousel.css$",
                            Callable = (request) =>
                            {
                                var response = new HttpResponse()
                                {
                                    StatusCode = ResponseStatusCode.Ok,
                                    ContentAsUTF8 = File.ReadAllText("../../content/css/carousel.css")
                                };
                                response.Header.ContentType = "text/css";
                                return response;
                            }
                        },
                        new Route()
                        {
                            Name = "Main CSS",
                            Method = RequestMethod.GET,
                            UrlRegex = "/css/main.css$",
                            Callable = (request) =>
                            {
                                var response = new HttpResponse()
                                {
                                    StatusCode = ResponseStatusCode.Ok,
                                    ContentAsUTF8 = File.ReadAllText("../../content/css/main.css")
                                };
                                response.Header.ContentType = "text/css";
                                return response;
                            }
                        },

                        new Route()
                        {
                            Name = "Bootstrap JS",
                            Method = RequestMethod.GET,
                            UrlRegex = "/bootstrap/js/bootstrap.min.js$",
                            Callable = (request) =>
                            {
                                var response = new HttpResponse()
                                {
                                    StatusCode = ResponseStatusCode.Ok,
                                    ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/js/bootstrap.min.js")
                                };
                                response.Header.ContentType = "application/x-javascript";
                                return response;
                            }
                        },
                        new Route()
                    {
                        Name = "Bootstrap CSS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/bootstrap/css/bootstrap.min.css$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse()
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css")
                            };
                            response.Header.ContentType = "text/css";
                            return response;
                        }
},
                        new Route()
                {
                    Name = "Custom CSS's",
                    Method = RequestMethod.GET,
                    UrlRegex = @"^/css/.+\.css$",
                    Callable = (request) =>
                    {
                        string nameOfCssFile = request.Url.Substring(request.Url.LastIndexOf('/') + 1);
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText($"../../Content/css/{nameOfCssFile}")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
                        new Route()
                        {
                            Name = "Controller/Action/GET",
                            Method = RequestMethod.GET,
                            UrlRegex = @"^/(.+)/(.+)$",
                            Callable = new ControllerRouter().Handle
                        },
                        new Route()
                        {
                            Name = "Controller/Action/POST",
                            Method = RequestMethod.POST,
                            UrlRegex = @"^/(.+)/(.+)$",
                            Callable = new ControllerRouter().Handle
                        }
                };
            }
        }
    }
}
