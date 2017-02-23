using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();  // permette di mappare gli attributi definiti direttamente dentro i vari Controller (vedi commento qui sotto).





            /*
             * Poichè, ogni volta che si cambia nome alla Action del controller, sarebbe necessario tornare qui sul RouteConfig e andare a cambiare manualmente la relativa action in modo che abbia lo stesso nome,
             * nelle ultime versioni di MVC, a partire da MVC5, microsoft ha introdotto un altro modo di creare un CustomLayout.
             * Questo nuovo metodo prevede l'uso di un attributo sopra la corrispondente Action, da inserire direttamente dentro il Controller (vedi  la action ByReleaseYear dentro MoviesController).
             * Qui nel RouteConfig basterà inserire:  
             * 
             *     routes.MapMvcAttributeRoutes();
             * 
             * Questo nuovo metodo permette inoltre di avere molto meno codice dentro il RouteConfig, mantenendolo più pulito.
            */

            // *******************************************************************************************************************************************************
            //              ### Questo codice non serve più perchè da MVC5 in poi il routing viene definito tramite attributi nel Controller stesso ###

            //// Definisco un CustomLayout
            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    "movies/released/{year}/{month}",
            //    new { controller = "Movies", action = "ByReleaseDate" },
            //    new { year = @"\d{4}", month = @"\d{2}" } // regular expression per indicare che l'anno deve avere 4 numeri (diit) e il mese 2 numeri
            //);

            // *******************************************************************************************************************************************************





            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
