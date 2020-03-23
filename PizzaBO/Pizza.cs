using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzas_BO
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage ="Le nom de la pizza doit avoir entre 5 et 20 caractères", MinimumLength = 5)]
        public string Nom { get; set; }

        public Pate Pate { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public static List<Ingredient> IngredientsDisponibles => new List<Ingredient>
        {
            new Ingredient{Id=1,Nom="Tomate"},
            new Ingredient{Id=2,Nom="Mozzarella"},
            new Ingredient{Id=3,Nom="Basilic"},
            new Ingredient{Id=4,Nom="Huile d'olive"},
            new Ingredient{Id=5,Nom="Jambon"},
            new Ingredient{Id=6,Nom="Champignons"},
            new Ingredient{Id=7,Nom="Anchois"},
            new Ingredient{Id=8,Nom="Olives noires"},
            new Ingredient{Id=9,Nom="Origan"},
            new Ingredient{Id=10,Nom="Ail"},
            new Ingredient{Id=11,Nom="Salami"}
        };

        public static List<Pate> PatesDisponibles => new List<Pate>
        {
            new Pate{ Id=1,Nom="Pate fine, base crême"},
            new Pate{ Id=2,Nom="Pate fine, base tomate"},
            new Pate{ Id=3,Nom="Pate épaisse, base crême"},
            new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
        };
    }
}
