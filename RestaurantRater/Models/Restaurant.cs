using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    //Entity = keyword. For now we are thinking of them as the models that get stored in the DB. Different models will get stored in different places.
    //Normally models just get passed back and forth from controller and view BUT This is our DATA level class it is what gets stored in our class 

    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Rating { get; set; }

    }

    //Identity Models we are not using that AppDbContext because we are working with one that we create here 
    //Have to call in DbContext using directive (System.Data.Entity)

    public class RestaurantDbContext : DbContext
    {
        //This creates a new collection of Restaurant... called Restaurants. This is setting up the snapshot of the database table for us to interact with (i.e. save data to) 
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}