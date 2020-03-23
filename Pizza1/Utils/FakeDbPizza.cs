using Pizzas_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza1.Utils
{
    public class FakeDbPizza
    {
        private static FakeDbPizza _instance;
        static readonly object instanceLock = new object();

        private FakeDbPizza()
        {
            pizzas = this.GetListPizzas();
            ingredients = Pizza.IngredientsDisponibles;
            pates = Pizza.PatesDisponibles;
            
        }

        public static FakeDbPizza Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FakeDbPizza();
                        }
                    }
                }
                return _instance;
            }
        }

        private List<Pizza> pizzas;
        private List<Ingredient> ingredients;
        private List<Pate> pates;

        public List<Pizza> Pizzas
        {
            get { return pizzas; }
        }

        private List<Pizza> GetListPizzas()
        {
            var i = 1;
            return new List<Pizza>
            {
                new Pizza{
                    Id = i++, Nom= "Margherita",
                    Ingredients = new List<Ingredient>{
                        Pizza.IngredientsDisponibles[0],
                        Pizza.IngredientsDisponibles[1],
                        Pizza.IngredientsDisponibles[2],
                        Pizza.IngredientsDisponibles[3]
                    },
                    Pate = Pizza.PatesDisponibles[0]
                },

                new Pizza{
                    Id = i++, Nom= "Reine",
                    Ingredients = new List<Ingredient>{
                        Pizza.IngredientsDisponibles[0],
                        Pizza.IngredientsDisponibles[1],
                        Pizza.IngredientsDisponibles[4],
                        Pizza.IngredientsDisponibles[5]
                    },
                    Pate = Pizza.PatesDisponibles[1]
                },
                
                new Pizza{
                    Id = i++, Nom= "Napolitaine",
                    Ingredients = new List<Ingredient>{
                        Pizza.IngredientsDisponibles[0],
                        Pizza.IngredientsDisponibles[1],
                        Pizza.IngredientsDisponibles[6],
                        Pizza.IngredientsDisponibles[7],
                        Pizza.IngredientsDisponibles[8],
                        Pizza.IngredientsDisponibles[3]
                    },
                    Pate = Pizza.PatesDisponibles[0]
                },
                
                new Pizza{
                    Id = i++, Nom= "Marinara",
                    Ingredients = new List<Ingredient>{
                        Pizza.IngredientsDisponibles[0],
                        Pizza.IngredientsDisponibles[9],
                        Pizza.IngredientsDisponibles[8],
                        Pizza.IngredientsDisponibles[3]
                    },
                    Pate = Pizza.PatesDisponibles[2]
                },

                new Pizza{
                    Id = i, Nom= "Diavola",
                    Ingredients = new List<Ingredient>{
                        Pizza.IngredientsDisponibles[0],
                        Pizza.IngredientsDisponibles[1],
                        Pizza.IngredientsDisponibles[10]
                    },
                    Pate = Pizza.PatesDisponibles[3]
                }

            };
        }
    }
}