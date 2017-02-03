using System;
using System.IO;

namespace Calculator
{
    class Calculator
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Content-type: text/html\r\n");

            //string fileContent = File.ReadAllText("Calculator.html");
            //Console.WriteLine(fileContent);

            Console.WriteLine("Content-type:text/html\r\n\r\n");
            Console.WriteLine("<form action=\"Calculator.exe\" method=\"post\">" +
                "<input type=\"text\" name=\"firstNumber\">" +
                "<input type=\"text\" name=\"sign\">" +
                "<input type=\"text\" name=\"secondNumber\">" +
                "<input type=\"submit\" value=\"Calculate\">" +
                "</form>");

            string getContent = Console.ReadLine();
            //string getContent = Environment.GetEnvironmentVariable("QUERY_STRING");
            string[] details = getContent.Split('&');
            decimal firstNumber = decimal.Parse(details[0].Split('=')[1]);
            string sign = details[1].Split('=')[1];
            decimal secobdNumber = decimal.Parse(details[2].Split('=')[1]);
            string result;

            switch (sign)
            {
                case "%2B": result = (firstNumber + secobdNumber).ToString(); break;
                case "*": result = (firstNumber * secobdNumber).ToString(); break;
                case "%2F": result = (firstNumber / secobdNumber).ToString(); break;
                case "-": result = (firstNumber - secobdNumber).ToString(); break;
                default: result = "Invalid sign"; break;
            }

            if (result == "Invalid sign")
            {
                Console.WriteLine($"<p> {result} </p> ");
            }
            else
            {
                Console.WriteLine($"<p> Result: {result} </p> ");
            }           

        }
    }
}
