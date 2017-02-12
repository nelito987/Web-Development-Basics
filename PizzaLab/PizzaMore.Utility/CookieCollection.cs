using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public class CookieCollection : ICookieCollection
    {       

        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public IDictionary<string, Cookie> Cookies { get; private set; }

        public Cookie this[string key]
        {
            get
            {
                return this.Cookies[key];
            }

            set
            {
                if (this.Cookies.ContainsKey(key))
                {
                    this.Cookies[key] = value;
                }
                else
                {
                    this.Cookies.Add(key, value);
                }
            }
        }

        public int Count
        {
            get
            {
                return this.Cookies.Count;
            }
        }

        public void AddCookie(Cookie cookie)
        {
            if (!this.Cookies.ContainsKey(cookie.Name)) 
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }
            this.Cookies[cookie.Name] = cookie;
        }

        public bool ContainsKey(string key)
        {
            return this.Cookies.ContainsKey(key);
        }

        public void RemoveCookie(string cookieName)
        {
            this.Cookies.Remove(cookieName);
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator<Cookie> IEnumerable<Cookie>.GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }
    }
}
