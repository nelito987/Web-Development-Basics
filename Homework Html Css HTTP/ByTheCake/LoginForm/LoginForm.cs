using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm
{
    class LoginForm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type:text/html\r\n\r\n");
            Console.WriteLine("<form action=\"LoginForm.exe\" method=\"post\">" +
                "<input type=\"text\" name=\"username\" placeholder=\"Username\"></br>" +
                "<input type=\"password\" name=\"password\" placeholder=\"Password\"></br>" +               
                "<input type=\"submit\" value=\"Login\">" +
                "</form>");

            string getContent = Console.ReadLine();
            string[] details = getContent.Split('&');
            string username = details[0].Split('=')[1];
            string password = details[1].Split('=')[1];

            Console.WriteLine($"Hi {username}, your password is {password}");

        }
    }
}
