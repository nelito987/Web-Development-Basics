using SharpStore.Data;
using SimpleHttpServer.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorNew;
using SharpStore.PageModels;
using SimpleHttpServer.Utilities;
using SharpStore.Services;
using System;
using System.Reflection;
using SimpleHttpServer.Enums;

namespace SharpStore
{
    class RoutesConfig
    {
        public static IList<Route> GetRoutes()
        {
            var routes = new List<Route>()
            {
                new Route()
                {
                    Name = "Home Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = new HomePage().ToString()
                        };
                    }
                },
                new Route()
                {
                    Name = "Home Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/.+?\\?theme=.+$",
                    Callable = (request) =>
                    {
                        var indexOfQ = request.Url.IndexOf('?');
                        var themeDict = QueryStringParser.Parse(request.Url.Substring(indexOfQ + 1));
                        var htmlName = request.Url.Substring(1, indexOfQ -1);
                        var typeOfNeededPage = Assembly.GetAssembly(typeof(RazorNew.Page))
                        .GetTypes()
                        .FirstOrDefault(t => t.Name.Contains(
                            htmlName[0].ToString().ToUpper() + htmlName.Substring(1)));

                        Page instance = (Page)Activator.CreateInstance(typeOfNeededPage);
                        var responce = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = instance.ToString()
                        };
                        responce.Header.Cookies.Add(new Cookie("theme", themeDict["theme"]));
                        return responce;
                    }
                },
                new Route()
                {
                    Name = "About Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/about$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/about.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Products Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/products.*$",
                    Callable = (request) =>
                    {
                        KnivesService service = new KnivesService();  
                        var products = service.GetAllKnivesFromUrl(request.Url);
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = new ProductsPage(products).ToString()
                        };
                    }
                },
                new Route()
                {
                    Name = "Contacts Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/contacts.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Contacts Post",
                    Method = RequestMethod.POST,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        string query = request.Content;
                        new MessagesService().AddMessageFromFormData(query);

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/contacts.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Carousel CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/content/css/carousel.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/css/carousel.css")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
                new Route()
                {
                    Name = "Products CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/content/css/products.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/css/products.css")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
                new Route()
                {
                    Name = "Bootstrap JS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/bootstrap/js/bootstrap.min.js$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
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
                    UrlRegex = "^/bootstrap/css/bootstrap.min.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
            };

            return routes;
        }
    }
}
