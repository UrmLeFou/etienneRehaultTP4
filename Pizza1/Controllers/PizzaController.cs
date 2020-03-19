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
                Pizza pizza = vm.Pizza;

                pizza.Pate = Pizza.PatesDisponibles.FirstOrDefault(p => p.Id == vm.IdPate);
                pizza.Ingredients = Pizza.IngredientsDisponibles.Where(p => vm.IdIngredients.Contains(p.Id)).ToList();

                pizza.Id = FakeDbPizza.Instance.Pizzas.Count == 0 ? 1 : FakeDbPizza.Instance.Pizzas.Max(p => p.Id) + 1;


                FakeDbPizza.Instance.Pizzas.Add(pizza);
                return RedirectToAction("Index");
            }
            catch
            {
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
