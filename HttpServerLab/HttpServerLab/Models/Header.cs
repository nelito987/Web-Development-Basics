using HttpServerLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerLab.Models
{
    public class Header
    {
        public Header(HeaderType headerType)
        {
            this.ContentType = "text/html";
            this.Type = headerType;
            this.Cookies = new CookieCollection();
            this.OtherParameters = new Dictionary<string, string>();
        }
        public HeaderType Type { get; set; }

        public string ContentType { get; set; }
        public string ContentLength { get; set; }

        public CookieCollection Cookies { get; private set; }

        public Dictionary<string, string> OtherParameters { get; set; }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();
            header.AppendLine($"Content-type: {this.ContentType}");

            if(this.Cookies.CookiesCount > 0)
            {
                if(this.Type == HeaderType.HttpRequest)
                {
                    header.AppendLine($"Cookie: {this.Cookies.ToString()}");
                }
                else if(this.Type == HeaderType.HttpResponse)
                {
                    foreach (var cookie in this.Cookies)
                    {
                        header.AppendLine($"Set-Cookie: {cookie}");
                    }                    
                }
                if(this.ContentLength != null)
                {
                    header.AppendLine($"Content-Length: {this.ContentLength}");
                }
                foreach (var param in this.OtherParameters)
                {
                    header.AppendLine($"{param.Key}: {param.Value}");
                }
                header.AppendLine();
                header.AppendLine();
            }

            return header.ToString();
        }
    }
}
