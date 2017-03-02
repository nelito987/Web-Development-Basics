using System.Collections.Generic;
using System.Text;

namespace PizzaForumApp.ViewModels
{
    public class AllViewModel
    {
        public LoggedInUserViewModel User { get; set; }
        public IEnumerable<AllCategoryViewModel> Categories { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (AllCategoryViewModel allCategoryViewMdoel in Categories)
            {
                sb.Append(allCategoryViewMdoel);
            }
            return sb.ToString();
        }
    }
}
