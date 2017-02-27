using PizzaMoreMVC.Models;
using System.Collections.Generic;

namespace PizzaMoreMVC.ViewModels
{
    public class PizzaSuggestionModel
    {
        public string Email { get; set; }
        public ICollection<Pizza> PizzaSuggestions { get; set; }
    }
}
