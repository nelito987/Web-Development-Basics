using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public static class Logger
    {
        public static void Log(string message)
        {
            File.WriteAllText("log.txt", message + Environment.NewLine);
        }
    }
}
