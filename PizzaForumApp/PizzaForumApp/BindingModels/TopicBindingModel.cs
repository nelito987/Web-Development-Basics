using System;
using System.Collections.Generic;
using PizzaForumApp.Models;

namespace PizzaForumApp.BindingModels
{
    public class TopicBindingModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
    }
}
