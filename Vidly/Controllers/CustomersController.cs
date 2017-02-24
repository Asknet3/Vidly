using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = new List<Customer> // Lista di Customers
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" },
                new Customer { Name = "Customer 3" },
                new Customer { Name = "Customer 4" },
                new Customer { Name = "Customer 5" }
            };

            /* L'oggetto "customers" appena creato verrà quindi passato alla view result.
               Creo quindi un'istanza dell'oggetto view model */
            var viewModel = new IndexCustomerViewModel
            {
                Customers = customers // Inizializzo la proprietà Customers della ViewModel passandogli la lista di customers
            };


            return View(viewModel); // Verificare sempre che nella View sia definito il model corretto. (In questo caso verificare nella View Index.cshtml)
        }
    }
}