using System.Collections;
using System.Collections.Generic;

namespace HttpServerLab.Models
{
    public class CookieCollection:IEnumerable<Cookie>
    {
        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }
        public IDictionary<string, Cookie> Cookies { get; private set; }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void AddCookie(Cookie cookie)
        {
            if (!this.Cookies.ContainsKey(cookie.Name))
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }
            this.Cookies[cookie.Name] = cookie;
        }

        public bool Contains(string cookieName)
        {
            return this.Cookies.ContainsKey(cookieName);
        }

        public int CookiesCount
        {
            get { return this.Cookies.Count; }
        }

        public Cookie this[string cookieName]
        {
            get
            {
                return this.Cookies[cookieName];
            }
            set
            {
                this.AddCookie(value);
            }
        }

        public override string ToString()
        {
            return string.Join("; ", Cookies.Values);
        }
    }
}
