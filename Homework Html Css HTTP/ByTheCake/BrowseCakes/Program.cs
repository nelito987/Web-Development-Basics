using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseCakes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type: text/html\r\n");

            string fileContent = File.ReadAllText("browseCakes.html");
            Console.WriteLine(fileContent);

            string getContent = Environment.GetEnvironmentVariable("QUERY_STRING");
            string cakeName = getContent.Split('=')[1];
            string[] databaseContent = File.ReadAllLines("databaseCakes.csv");
            var result = databaseContent.Where(s => s.Contains(cakeName));

            foreach (var cake in result)
            {
                Console.WriteLine(cake);
            }
        }
    }
}
