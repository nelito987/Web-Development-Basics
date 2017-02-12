using PizzaMore.Data;
using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSuggestion
{
    class YourSuggestions
    {
        private static IDictionary<string, string> PostParams;
        private static Header Header = new Header();
        private static Session Session;
        static void Main(string[] args)
        {
            Session = WebUtil.GetSession();
            if (Session == null)
            {
                Header.Print();
                WebUtil.PageNotAllowed();
                return;
            }

            if (WebUtil.IsGet())
            {
                ShowPage();
            }
            else if (WebUtil.IsPost())
            {
                DeletePizza();
                ShowPage();
            }
        }

        private static void DeletePizza()
        {
            PostParams = WebUtil.RetrievePostParameters();
            using(var ctx = new PizzaMoreContext())
            {
                var pizza = ctx.PizzaSuggestions.Find(int.Parse(PostParams["pizzaid"]));
                ctx.PizzaSuggestions.Remove(pizza);
                ctx.SaveChanges();
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("views/yoursuggestions-top.html");
            PrintListOfSuggestedItems();
            WebUtil.PrintFileContent("views/yoursuggestions-bottom.html");
        }

        private static void PrintListOfSuggestedItems()
        {
            var ctx = new PizzaMoreContext();
            //var suggestions = ctx.PizzaSuggestions.Where(p => p.OwnerId == Session.UserId);
            var suggestions = ctx.Users.Find(Session.UserId).Suggestions;
            Console.WriteLine("<ul>");
            foreach (var suggestion in suggestions)
            {
                Console.WriteLine("<form method=\"POST\">");
                Console.WriteLine($"<li><a href=\"DetailsPizza.exe?pizzaid={suggestion.Id}\">{suggestion.Title}</a> <input type=\"hidden\" name=\"pizzaId\" value=\"{suggestion.Id}\"/> <input type=\"submit\" class=\"btn btn-sm btn-danger\" value=\"X\"/></li>");
                Console.WriteLine("</form>");
            }
            Console.WriteLine("</ul>");

        }
    }
}
