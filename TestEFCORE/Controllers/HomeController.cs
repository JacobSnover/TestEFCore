using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestEFCORE.Models;

namespace TestEFCORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Use my context class to pull in my DataBase data
            TestEfCoreContext db = new TestEfCoreContext();

            //pass the model to view to display the data
            return View(db);
        }

        // this action is used to determine which view to load
        public IActionResult WelcomeViews()
        {
            // use a conditional statement to determine which view to load
            if (true)
                //this will return the WelcomeYou view
                return WelcomeYou();// you don't have to pass arguements
            // return WelcomeYou(string name); you can also pass arguements
            else
                // this will return the WelcomeMe view
                return WelcomeMe();
        }

        // these views, WelcomeYou, and WelcomeMe are called by above method determined by the conditional statetment
        public IActionResult WelcomeYou()
        {
            return View("WelcomeYou", "Bobby");
        }

        public IActionResult WelcomeMe()
        {
            return View("WelcomeMe", "Me");
        }


        // build a new action that will take my users input
        // make two inputs of type string, and use same names from HTML inputs
        // to insure proper mapping
        //public IActionResult Hello(string name, string age)// make a string parameter to accept HTML text box input
        public IActionResult Hello(Person per)//make a Person object as the parameter, 
                                             //and let the framework map the values
        {
            // grab my user input and store it in a variable
            // string UserInput = userInput;

            // make to ViewBag objects to hold my two parameters
            // as long as this name hasn't been used I can name my ViewBag
            //ViewBag.Name = name;
            //ViewBag.Age = age;

            return View(per);
        }

        //make a new action that will search the DB for my result
        public IActionResult Search(string result)
        {
            // Use my context class to pull in my DataBase data
            TestEfCoreContext db = new TestEfCoreContext();

            // make an indiviodual Person object to store my result in
            Person foundResult = new Person();

            // i need to find my result in my DB
            foreach(Person person in db.Person)
            {
                // as i iterate through the collection, I want to find the correct result
                if(person.Name == result)
                {
                    // if you find a match, assign that value to your temp Person object
                    foundResult = person;
                }
            }
            // pass the object with the data to the view to be displayed
            return View(foundResult);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
