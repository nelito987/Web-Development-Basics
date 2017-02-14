using HttpServerLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
namespace HttpServerLab.Models
{
    public class Route
    {
        public string Name { get; set; }

        public string UrlRegex { get; set; }

        public RequestMethod RequestMethod { get; set; }

        public Func<HttpRequest, HttpResponse> Callable { get; set; }
    }
}
