using System.Collections.Generic;

namespace SimpleHttpServer.Models
{
    public class HttpSession
    {
        public HttpSession(string id)
        {
            this.parameters = new Dictionary<string, string>();
            this.Id = id;
        }
        public string Id { get; set; }
        public IDictionary<string, string> parameters { get; set; }

        public string this[string key]
        {
            get { return this.parameters[key]; }
        }
        public void Clear()
        {
            this.parameters.Clear();
        }

        public void Add(string key, string value)
        {
            this.parameters.Add(key, value);
        }
    }
}
