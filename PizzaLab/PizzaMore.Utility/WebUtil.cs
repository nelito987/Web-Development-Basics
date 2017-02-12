using PizzaMore.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public static class WebUtil
    {
        public static bool IsPost()
        {
            var enviornmentVariable = Environment.GetEnvironmentVariable(Constants.RequestMethod);
            if(enviornmentVariable != null)
            {
                string requestMethod = enviornmentVariable.ToLower();
                if(requestMethod == "post")
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsGet()
        {
            var enviornmentVariable = Environment.GetEnvironmentVariable(Constants.RequestMethod);
            if (enviornmentVariable != null)
            {
                string requestMethod = enviornmentVariable.ToLower();
                if (requestMethod == "get")
                {
                    return true;
                }
            }
            return false;
        }

        public static IDictionary<string, string> RetrieveRequestParameters(string parametersString)
        {
            Dictionary<string, string> resultParameters = new Dictionary<string, string>();
            var parameters = parametersString.Split('&');
            foreach (var parameter in parameters)
            {
                var pair = parameter.Split('=');
                var name = pair[0];
                string value = null;
                if(pair.Length > 1)
                {
                    value = pair[1];
                }
                resultParameters.Add(name, value);
            }

            return resultParameters;
        }

        public static IDictionary<string, string> RetrieveGetParameters()
        {
            string parametersString = WebUtility.UrlDecode(Environment.GetEnvironmentVariable(Constants.QueryString));
            return RetrieveRequestParameters(parametersString);
        }

        public static IDictionary<string, string> RetrievePostParameters()
        {
            string parametersString = WebUtility.UrlDecode(Console.ReadLine());
            return RetrieveRequestParameters(parametersString);
        }

        public static ICookieCollection GetCookies()
        {
            string cookieString = Environment.GetEnvironmentVariable(Constants.HttpCookie);

            if (string.IsNullOrEmpty(cookieString))
            {
                return new CookieCollection();
            }

            var cookies = new CookieCollection();
            string[] cookieSaves = cookieString.Split(';');

            foreach (var cookieSave in cookieSaves)
            {
                string[] cookiePair = cookieSave.Split('=');
                var cookie = new Cookie(cookiePair[0].Trim(), cookiePair[1].Trim());
                cookies.AddCookie(cookie);
            }

            return cookies;
        }

        public static Session GetSession()
        {
            var cookies = GetCookies();
            if (!cookies.ContainsKey(Constants.SessionIdKey))
            {
                return null;
            }

            var sessionCookie = cookies[Constants.SessionIdKey];
            var ctx = new PizzaMoreContext();

            var session = ctx.Sessions.FirstOrDefault(s => s.Id == sessionCookie.CookieValue);
            return session;
        }

        public static void PageNotAllowed()
        {
            PrintFileContent("game/index.html");
        }

        public static void PrintFileContent(string path)
        {
            string content = File.ReadAllText(path);
            Console.WriteLine(content);
        }
    }
}
