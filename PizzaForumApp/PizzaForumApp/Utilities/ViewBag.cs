using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaForumApp.Utilities
{
    static class ViewBag
    {
        public static Dictionary<string, dynamic> Bag = new Dictionary<string, dynamic>();

        public static dynamic GetUserName()
        {
            if (Bag.ContainsKey("username"))
            {
                return Bag["username"];
            }

            return null;
        }
    }
}
