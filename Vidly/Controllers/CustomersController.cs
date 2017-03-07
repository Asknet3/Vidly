using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();  // Inizializzo il dbcontext
        }


        // Adesso dobbiamo fare il Dispose dell'oggetto _context
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        //// GET: Customers
        //public ActionResult Index()
        //{
        //    var viewModel = new IndexCustomerViewModel
        //    {
        //        Customers = GetCustomers() // Inizializzo la proprietà Customers della ViewModel passandogli la lista di customers
        //    };


        //    return View(viewModel); // Verificare sempre che nella View sia definito il model corretto. (In questo caso verificare nella View Index.cshtml)
        //}

        // GET: Customers



        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            /* Usando EntityFramework, capita spesso di usare delle Data Annotations (es. [Required] oppure [StringLength(255)])
             * come attributi nel Model. Questi andranno ad agire creando i campi nel DB con le appropriate regole definite.
             * Per rispettare tali regole, è bene usare delle Validazioni direttamente nelle Form e questo si fa usando i ModelState
             * come di seguito
            */        
            if(!ModelState.IsValid)   // Il ModelState non è valido, restituisco la stessa View.
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id); // Estraggo il customer dal DB usando la lambda expression

            if (customer == null)
                return HttpNotFound(); // Restituisco un 404 se il customer non esiste

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        

        public ViewResult Index()
        {
            /* commento la valorizzazione della lista di customers perchè adesso è stata implementata lato client con una chiamata ajax alla relativa WebApi
             * 
            // Inizializzo la variabile customers prendendo i dati dal DB
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();  // E.F. carica solo gli oggetti customer e non quelli collegati (es. MembershipType). 
                                                                                         // Per questo motivo è necessario  caricare i customers e i relativi membershiptypes 
                                                                                         // assieme (il cosiddetto Eager Loading)

            return View(customers); // Verificare sempre che nella View sia definito il model corretto. (In questo caso verificare nella View Index.cshtml)
            */

            return View();


        }




        [Route("customers/details/{id}")]  // Attributo che permette di definire il Routing sostituendo di fatto il dover scrivere codice dentro RouteConfig
        public ActionResult Details(int Id)
        {
            var details = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);
            return View(details);
        }




        //// Get All Customers
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id=0, Name = "John Smith" },
        //        new Customer { Id=1, Name = "Mary Williams" },
        //        new Customer { Id=2, Name = "Customer 3" },
        //        new Customer { Id=3, Name = "Customer 4" },
        //        new Customer { Id=4, Name = "Customer 5" }
        //    };
        //}




        //// Get Customer by Id
        //private Customer GetCustomer(int Id)
        //{
        //    IEnumerable<Customer> allCustomers = GetCustomers();

        //    foreach (Customer customer in allCustomers)
        //    {
        //        if (customer.Id == Id)
        //        {
        //            return customer;
        //            break;
        //        }
        //    }

        //    return null;
        //}
    }
}