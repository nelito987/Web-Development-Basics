using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public class Cookie
    {
        private string name;
        private string cookieValue;

        public Cookie()
        {
            this.Name = null;
            this.CookieValue = null;
        }

        public Cookie(string name, string cookieValue)
        {
            this.Name = name;
            this.CookieValue = cookieValue;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string CookieValue
        {
            get { return this.cookieValue; }
            set { this.cookieValue = value; }
        }

        public override string ToString()
        {
            string cookie = this.Name + "=" + this.CookieValue;
            return cookie;
        }
    }
}
