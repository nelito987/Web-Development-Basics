namespace Home
{
    using PizzaMore.Data;
    using PizzaMore.Utility;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    internal class Home
    {
        private static IDictionary<string, string> RequestParameters;
        private static Session Session;
        private static Header Header = new Header();
        private static string Language;

        static void Main()
        {
            AddDefaultLanguageCookie();

            if (WebUtil.IsGet())
            {
                RequestParameters = WebUtil.RetrieveGetParameters();
                TryLogOut();
                Language = WebUtil.GetCookies()["lang"].CookieValue;
            }
            else if (WebUtil.IsPost())
            {
                RequestParameters = WebUtil.RetrievePostParameters();
                Header.AddCookie(new Cookie("lang", RequestParameters["language"]));
                Language = RequestParameters["language"];
            }

            ShowPage();
        }

        private static void TryLogOut()
        {
            RequestParameters = WebUtil.RetrieveGetParameters();
            if (RequestParameters.ContainsKey("logout"))
            {
                if(RequestParameters["logout"] == "true")
                {
                    Session = WebUtil.GetSession();
                    using(var ctx = new PizzaMoreContext())
                    {
                        var session = ctx.Sessions.Find(Session.Id);
                        ctx.Sessions.Remove(session);
                        ctx.SaveChanges();
                    }
                }
            } 
        }

        private static void AddDefaultLanguageCookie()
        {
            if (!WebUtil.GetCookies().ContainsKey("lang"))
            {
                Header.AddCookie(new Cookie("lang", "EN"));
                Language = "EN";
                ShowPage();
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            if (Language.Equals("DE"))
            {
                ServeHtmlBg();
            }
            else
            {
                ServeHtmlEn();
            }
        }

        private static void ServeHtmlBg()
        {
            WebUtil.PrintFileContent("views/home-de.html");
        }

        private static void ServeHtmlEn()
        {
            WebUtil.PrintFileContent("views/home.html");
        }

    }

}
