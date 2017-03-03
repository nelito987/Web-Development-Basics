using System.Collections.Generic;

namespace PizzaForumApp.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsTopicViewModel Topic { get; set; }

        public IEnumerable<DetailsReplyViewModel> Replies { get; set; }
    }
}
