using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public class Header
    {
        public Header()
        {
            this.Type = "Content-type: text/html";
            this.Cookies = new CookieCollection();
        }
        public string Type { get; set; }

        public string Location { get; set; }

        public ICookieCollection Cookies { get; set; }

        public void AddLocation(string location)
        {
            this.Location = $"Location: {location}";
        }

        public void AddCookie(Cookie cookie)
        {
            this.Cookies.AddCookie(cookie);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Type);

            if (this.Cookies.Any())
            {
                foreach (var cookie in this.Cookies)
                {
                    sb.AppendLine($"Set-Cookie: {cookie.ToString()}");
                }
            }

            if (this.Location != null)
            {
                sb.AppendLine(this.Location);
            }

            sb.AppendLine();
            sb.AppendLine();

            return sb.ToString();
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
