using Pizza1.Models;
using Pizza1.Utils;
using Pizzas_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizza1.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDbPizza.Instance.Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Id.Equals(id));

            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToRoute("Index");
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaVM vm = new PizzaVM();
            vm.Pates = Pizza.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
            vm.Ingredients = Pizza.IngredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();

            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM vm)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    bool isValid = true;

                    Pizza pizza = vm.Pizza;

                    pizza.Pate = Pizza.PatesDisponibles.FirstOrDefault(p => p.Id == vm.IdPate);
                    pizza.Ingredients = Pizza.IngredientsDisponibles.Where(p => vm.IdIngredients.Contains(p.Id)).ToList();

                    if (vm.Pizza.Ingredients.Count() < 2 || vm.Pizza.Ingredients.Count() > 5)
                    {
                        ModelState.AddModelError("", "La pizza doit avoir entre 2 et 5 ingrédients");
                        isValid = false;
                        
                    }

                    if (FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Nom == vm.Pizza.Nom) != null)
                    {
                        ModelState.AddModelError("", "Ce nom a déjà été donné");
                        isValid = false;
                    }

                    foreach (var pizzaDb in FakeDbPizza.Instance.Pizzas)
                    {
                        if (vm.IdIngredients.Count == pizza.Ingredients.Count)
                        {
                            bool isDifferent = false;

                            List<Ingredient> ingredientsDb = pizzaDb.Ingredients.OrderBy(p => p.Id).ToList();
                            vm.IdIngredients = vm.IdIngredients.OrderBy(i => i).ToList();

                            for (int i = 0; i < vm.IdIngredients.Count; i++)
                            {
                                if (vm.IdIngredients.ElementAt(i) != ingredientsDb.ElementAt(i).Id)
                                {
                                    isDifferent = true;
                                    break;
                                }
                            }

                            if (!isDifferent)
                            {
                                ModelState.AddModelError("", "Cette recette existe déjà");
                                isValid = false;
                            }
                        }
                    }

                    if (isValid == false)
                    {
                        vm.Pates = Pizza.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
                        vm.Ingredients = Pizza.IngredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
                        return View(vm);
                    }

                    pizza.Id = FakeDbPizza.Instance.Pizzas.Count == 0 ? 1 : FakeDbPizza.Instance.Pizzas.Max(p => p.Id) + 1;

                    FakeDbPizza.Instance.Pizzas.Add(pizza);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                vm.Pates = Pizza.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
                vm.Ingredients = Pizza.IngredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();

                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaVM vm = new PizzaVM();

            vm.Pates = Pizza.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
            vm.Ingredients = Pizza.IngredientsDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();

            vm.Pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Id == id);

            if (vm.Pizza.Pate != null)
            {
                vm.IdPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdIngredients = vm.Pizza.Ingredients.Select(p => p.Id).ToList();
            }
            
            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVM vm)
        {
            try
            {
                Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Id.Equals(vm.Pizza.Id));
                pizza.Nom = vm.Pizza.Nom;
                pizza.Pate = Pizza.PatesDisponibles.FirstOrDefault(p => p.Id.Equals(vm.IdPate));
                pizza.Ingredients = Pizza.IngredientsDisponibles.Where(p => vm.IdIngredients.Contains(p.Id)).ToList();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Id.Equals(id));

            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToRoute("Index");
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
                FakeDbPizza.Instance.Pizzas.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
