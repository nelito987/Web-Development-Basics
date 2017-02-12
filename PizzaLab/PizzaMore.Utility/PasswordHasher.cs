using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            return "SECRET" + password;
        }
    }
}
