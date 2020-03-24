using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : Controller
    {
        //Had to create this to be able to work with our stored Restaurants 
        private RestaurantDbContext _dB = new RestaurantDbContext();

        // GET: Resturant/Index 

        //This gives us an index action for our index view 
        //This built out the view folder but nothing was in the folder
        //Have to create an Index file in the folder
        public ActionResult Index()
        {
            //This view looks for the index.html inside the index file in the Resturant Folder
            return View(_dB.Restaurants.ToList());  //turns table into list 

            //Inserting the argument 
            //--> What it's doing is reaching into the _db field
            // --> Calling the Restaurants property (i.e. the DBset we created
            // --> converting it to a List wit
            // --> Passed into the View method which will then pass it to the View as the MODEL 
        }

        //GET: Restaurant/Create --> we must create this method in the controller so that the view can function (exist really)
        //This method GETs our view (if we don't have it we get the 404 error)
        //This Gets the view --> it just gets information 

        public ActionResult Create()
        {
            return View();
        }

        //Post: Restaurant/Create 
        //Once we have create method and an input field we now need to add a POST method so that the request can be sent through to the DB and back
        //This sumbits the data to the server

        [HttpPost] //specifies what this method is going to be
        [ValidateAntiForgeryToken] //matches token with create form

        //This method takes in the Restaurant parameter because it is the model the view is going to give the controller once the POST is called, so it needs the model to work with
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid) //checks the model to see if all requirements are met 
            {
                _dB.Restaurants.Add(restaurant); //adding the argument to the table 

                //takes our modified dBContext and saves changes to the actual SQL database
                //It also returns an int with the count of how many rows were modified 
                _dB.SaveChanges();

                //This kicks us over the Index view (page) 
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        //GET: Restaurant/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = _dB.Restaurants.Find(id);

            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);

        }

        //POST: Restaurant/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            Restaurant restaurant = _dB.Restaurants.Find(id);
            _dB.Restaurants.Remove(restaurant);
            _dB.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Restaurant/Edit/{id}
        //Get an id from the user
        //Handle if the id is null 
        //Find a restaurant by that id
        //If the restaurant doesn't exist 
        //Return the restaurant and the view 

        public ActionResult Edit(int? id)
        {
            //we want to try and think about ways the user could break the code and setup a way to catch the error and return a something. Here we fix if the type in the wrong ID or nothing at all 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = _dB.Restaurants.Find(id);

            if (restaurant == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(restaurant);
        }


        //POST: Restaurant/Edit/{id}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                //What this is doing is saying find the entry on the table...access it's state
                //Tell it has been modified 
                _dB.Entry(restaurant).State = EntityState.Modified;

                //So above says "hey there are updates to the row".... below says "ok, thanks, I'll update the DB" 
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant); 
        }


        //For Details (i.e. viewing lists of data) we don't need a POST method because we are not changing anything. Just need the GET method

        //GET: Restaurant/Detail/{id}

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = _dB.Restaurants.Find(id);

            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);

        }

    }
}