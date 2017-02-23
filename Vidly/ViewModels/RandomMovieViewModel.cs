using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    // Un ViewModel è un model costruito appositamente per una View (in questo caso per la View "Random"). Esso include tutti i dati e le regole specifiche per quella View.
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}