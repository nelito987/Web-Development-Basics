using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByTheCake
{
    class AddCake
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type: text/html\r\n");

            string fileContent = File.ReadAllText("index.html");
            Console.WriteLine(fileContent);

            string postContent = Console.ReadLine();
            Console.WriteLine(postContent);

            string[] variables = postContent.Split('&');
            string cakeName = variables[0].Split('=')[1];
            string cakePrice = variables[1].Split('=')[1];

            File.AppendAllText("databaseCakes.csv", $"{cakeName} , {cakePrice}" + Environment.NewLine);
        }
    }
}
