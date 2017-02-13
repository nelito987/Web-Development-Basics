using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerLab.Models
{
    public class Cookie
    {
        public Cookie():this(null, null)
        {
        }

        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}
