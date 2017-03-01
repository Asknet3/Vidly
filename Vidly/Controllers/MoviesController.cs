using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;


        public MoviesController()
        {
            _context = new ApplicationDbContext();  // Inizializzo il dbcontext
        }



        // Adesso dobbiamo fare il Dispose dell'oggetto _context
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
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
                Movie = _context.Movies.ToList(),
                Customers = _context.Customers.ToList()
            };

            //return View(movie);  // E' un helper method ereditato dalla classe base Controller. Questo metodo permette inoltre di creare facilmente una ViewResult. Alla View viene passato un Model (movie in questo caso) come parametro
            return View(viewModel); 
        }




        public ActionResult New ()
        {
            var genres = _context.Genres.ToList();  // Recupero la lista di generi
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),  // Questa riga è fondamentale per evitare di avere errori di validazione nella creazione di un nuovo Movie
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }
                

            if (movie.Id == 0)  // E' un nuovo Movie. Lo aggiungo al DB
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);  // Recupero il Movie dal DB

                //Aggiorno i campi:
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }



        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound(); // Se il Movie con quell'Id non esiste, restituisce un 404

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }





        //// Lista di tutti i Movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    //if (!pageIndex.HasValue)
        //    //    pageIndex = 1;  // Se non assegno alcun valore a pageIndex, di default verrà assegnato 1

        //    //if (String.IsNullOrWhiteSpace(sortBy))
        //    //    sortBy = "Name";    // Se non assegno alcun valore a sortBy, di default verrà assegnato "Name"

        //    //return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));

        //    movie = _context.Movies.Include(m => m.Genre).ToList();  // Inizializzo la lista di tutti i Movies prendendoli dal DB
        //    var viewmodel = new IndexMovieViewController
        //    {
        //        Movies = movie
        //    };


        //    return View(viewmodel);
        //}


        // Lista di tutti i Movies


        
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }




        [Route("movies/details/{id}")]  // Attributo che permette di definire il Routing sostituendo di fatto il dover scrivere codice dentro RouteConfig
        public ActionResult Details (int Id)
        {
            var details = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            return View(details);
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