using HttpServerLab.Enums;
using HttpServerLab.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpServerLab
{
    public class HttpProcessor
    {
        private HttpRequest Request;
        private HttpResponse Response;
        private IList<Route> Routes;

        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }

        public void HandleClient(TcpClient tcpClient)
        {
            using(var stream = tcpClient.GetStream())
            {
                Request = GetRequest(stream);
                Response = RouteRequest();
                StreamUtils.WriteResponse(stream, Response);
            }
        }

        private HttpResponse RouteRequest()
        {
            var routes = this.Routes.Where(x => Regex.Match(Request.Url, x.UrlRegex).Success).ToList();
            if (!routes.Any())
            {
                return HttpResponseBuilder.NotFound();
            }
            var route = routes.SingleOrDefault(x => x.RequestMethod == Request.Method);
            if(route == null)
            {
                return new HttpResponse()
                {
                    StatusCode = ResponseStatusCode.MethodNotAllowed
                };
            }

            try
            {
                return route.Callable(Request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return HttpResponseBuilder.InternalServerError();
            }
        }

        private HttpRequest GetRequest(Stream stream)
        {
            string requestLine = StreamUtils.ReadLine(stream);
            string[] requestParams = requestLine.Split(' ');
            if(requestParams.Length != 3)
            {
                throw new Exception("Invalid http request");
            }
            RequestMethod method = (RequestMethod)Enum.Parse(typeof(RequestMethod), requestParams[0].ToUpper());
            string url = requestParams[1];
            string protocolVersion = requestParams[2];

            Header header = new Header(HeaderType.HttpRequest);
            string line;
            while((line = StreamUtils.ReadLine(stream)) != null)
            {
                if(line == " ")
                {
                    break;
                }

                int separator = line.IndexOf(':');
                if(separator == -1)
                {
                    throw new Exception("Invalid http header" +line);
                }

                string name = line.Substring(0, separator);
                int pos = separator + 1;
                while((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }
                string value = line.Substring(pos, line.Length - pos);

                if(name == "Cookie")
                {
                    string[] cookies = value.Split(';');
                    foreach (var cookie in cookies)
                    {
                        string[] cookiePair = cookie.Split('=');
                        var cookieToAdd = new Cookie(cookiePair[0].Trim(), cookiePair[1].Trim());
                        header.Cookies.AddCookie(cookieToAdd);
                    }
                }
                else if (name == "Content-Length")
                {
                    header.ContentLength = value;
                }
                else
                {
                    header.OtherParameters.Add(name, value);
                }
            }

            string content = null;
            if(header.ContentLength != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLength);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = stream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }
                content = Encoding.ASCII.GetString(bytes);
            }

            var request = new HttpRequest()
            {
                Method = method,
                Url = url,
                Header = header,
                Content = content
            };
            Console.WriteLine("-REQUEST-----------------------------");
            Console.WriteLine(request);
            Console.WriteLine("------------------------------");

            return request;
        }
    }
}
