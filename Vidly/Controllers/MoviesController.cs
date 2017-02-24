using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // Dichiaro gli oggetti che verranno usati in tutta la classe
        public List<Movie> movie { get; set; }
        public List<Customer> customers;

        public MoviesController()
        {
            // Inizializzo gli oggetti che verranno usati in tutta la classe
            movie = new List<Movie>
            {
                new Movie { Name = "Shrek" },
                new Movie { Name = "Wall-e" }
            };

            customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };
        }

        
        // GET: Movies/Random
        public ActionResult Random  ()
        {
            /*
            La classe AcionResult può restituire diversi Type, ognuno dei quali ha un suo Helper Method. Esempio:
            Type                    |   Helper Method
            ViewResult              |   View()
            PartialViewResult       |   PartialView()
            ContentResult           |   Content()
            RedirectReult           |   Redirect()
            RedirectToRouteResult   |   RedirectToAction()
            JsonResult              |   Json()
            FileResult              |   File()
            HttpNotFoundResult      |   HttpNotFound()
            EmptyResult             |

            */
           
            // ViewData["Movie"] = movie;  // Ogni controller ha una proprietàchiamata ViewData che è di tipo ViewDataDictionary

            

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            //return View(movie);  // E' un helper method ereditato dalla classe base Controller. Questo metodo permette inoltre di creare facilmente una ViewResult. Alla View viene passato un Model (movie in questo caso) come parametro
            return View(viewModel); 
        } 


        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        //movies
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            //if (!pageIndex.HasValue)
            //    pageIndex = 1;  // Se non assegno alcun valore a pageIndex, di default verrà assegnato 1

            //if (String.IsNullOrWhiteSpace(sortBy))
            //    sortBy = "Name";    // Se non assegno alcun valore a sortBy, di default verrà assegnato "Name"

            //return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
            var viewmodel = new IndexMovieViewController
            {
                Movies = movie
            };
           

            return View(viewmodel);
        }


        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]  // Attributo che permette di definire il Routing sostituendo di fatto il dover scrivere codiceB dentro RouteConfig
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }
    } 
}