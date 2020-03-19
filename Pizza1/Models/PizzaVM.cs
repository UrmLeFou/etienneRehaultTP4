using Pizzas_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizza1.Models
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }

        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Pates { get; set; } = new List<SelectListItem>();

        public int IdPate { get; set; }
        public List<int> IdIngredients { get; set; } = new List<int>();
    }
}