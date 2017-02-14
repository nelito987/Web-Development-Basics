using HttpServerLab.Enums;
using System;
using System.Text;

namespace HttpServerLab.Models
{
    public class HttpResponse
    {
        public HttpResponse()
        {
            this.Header = new Header(HeaderType.HttpResponse);
            this.Content = new byte[] { };
        }

        public Header Header { get; set; }

        public byte[] Content { get; set; }
        public ResponseStatusCode StatusCode { get; set; }

        public string StatusMessage
        {
            get
            {
                return Enum.GetName(typeof(ResponseStatusCode), this.StatusCode);
            }
        }        

        public string ContentAsUTF8
        {
            set
            {
                this.Content = Encoding.UTF8.GetBytes(value);
            }
        }

        public override string ToString()
        {
            return string.Format("HTTP/1.0 {0} {1}\r\n{2}",
                (int)this.StatusCode,
                this.StatusMessage,
                this.Header);
        }

    }
}
