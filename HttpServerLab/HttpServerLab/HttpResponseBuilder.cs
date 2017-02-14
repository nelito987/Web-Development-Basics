using HttpServerLab.Enums;
using HttpServerLab.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerLab
{
    public static class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string content = File.ReadAllText("Resources/Pages/500.html");
            HttpResponse response = new HttpResponse()
            {
                ContentAsUTF8 = content,
                StatusCode = ResponseStatusCode.InternalServerError
            };

            return response;
        }

        public static HttpResponse NotFound()
        {
            string content = File.ReadAllText("Resources/Pages/404.html");
            HttpResponse response = new HttpResponse()
            {
                ContentAsUTF8 = content,
                StatusCode = ResponseStatusCode.NotFound
            };

            return response;
        }

    }
}
