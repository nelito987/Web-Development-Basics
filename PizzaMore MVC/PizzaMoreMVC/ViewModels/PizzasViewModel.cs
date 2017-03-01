using System.Collections.Generic;
using PizzaMoreMVC.Models;

namespace PizzaMoreMVC.ViewModels
{
    public class PizzasViewModel
    {
        public ICollection<Pizza> PizzaSuggestions { get; set; }
    }
}
