using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
       
        // GET: Customers
        public ActionResult Index()
        {
            var viewModel = new IndexCustomerViewModel
            {
                Customers = GetCustomers() // Inizializzo la proprietà Customers della ViewModel passandogli la lista di customers
            };


            return View(viewModel); // Verificare sempre che nella View sia definito il model corretto. (In questo caso verificare nella View Index.cshtml)
        }




        [Route("customers/details/{id}")]  // Attributo che permette di definire il Routing sostituendo di fatto il dover scrivere codice dentro RouteConfig
        public ActionResult Details(int Id)
        {
            var details = GetCustomer(Id);
            return View(details);
        }




        // Get All Customers
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id=0, Name = "John Smith" },
                new Customer { Id=1, Name = "Mary Williams" },
                new Customer { Id=2, Name = "Customer 3" },
                new Customer { Id=3, Name = "Customer 4" },
                new Customer { Id=4, Name = "Customer 5" }
            };
        }



        // Get Customer by Id
        private Customer GetCustomer(int Id)
        {
            IEnumerable<Customer> allCustomers = GetCustomers();

            foreach (Customer customer in allCustomers)
            {
                if (customer.Id == Id)
                {
                    return customer;
                    break;
                }
            }

            return null;
        }
    }
}