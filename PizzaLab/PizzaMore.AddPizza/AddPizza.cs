using PizzaMore.Data;
using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.AddPizza
{
    class AddPizza
    {
        private static Header Header = new Header();
        private static Session Session;
        private static IDictionary<string, string> PostParams;
        static void Main(string[] args)
        {
            Session = WebUtil.GetSession();
            if (Session != null)
            {
                if (WebUtil.IsGet())
                {
                    ShowPage();
                }
                else if (WebUtil.IsPost())
                {
                    PostParams = WebUtil.RetrievePostParameters();
                    var title = PostParams["title"];
                    var recipe = PostParams["recipe"];
                    var url = PostParams["url"];
                    using (var ctx = new PizzaMoreContext())
                    {
                        var user = ctx.Users.Find(Session.UserId);
                        var newPizza = new Pizza()
                        {
                            Title = title,
                            Recipe = recipe,
                            ImageUrl = url,
                            DownVotes = 0,
                            UpVotes = 0,
                            OwnerId = user.Id
                        };
                        user.Suggestions.Add(newPizza);
                        ctx.SaveChanges();
                    }
                    ShowPage();
                }
            }
            else
            {
                Header.Print();
                WebUtil.PageNotAllowed();                
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("views/addpizza.html");
        }
    }
}
