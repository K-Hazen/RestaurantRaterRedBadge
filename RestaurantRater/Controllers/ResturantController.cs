using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantRater.Controllers
{
    public class ResturantController : Controller
    {
        // GET: Resturant/Index 

        //This gives us an index action for our index view 
        //This built out the view folder but nothing was in the folder
        //Have to create an Index file in the folder
        public ActionResult Index()
        {
            //This view looks for the index.html inside the index file in the Resturant Folder
            return View();
        }
    }
}