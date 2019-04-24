using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using dental.Models;
using DentalEstrada.Models;
using Microsoft.AspNetCore.Identity;

namespace dental.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }



        //!!!!!!!!!!!  GETS
        //!!!!!!!!!!!  GETS
        //!!!!!!!!!!!  GETS
        //!!!!!!!!!!!  GETS
        //!!!!!!!!!!!  GETS



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS





        //!!!!!!!!!!!  ERRORS
        //!!!!!!!!!!!  ERRORS


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        //!!!!!!!!! ADMIN GETS & POSTS !!!!!!!!!!!!!!!!//
        //!!!!!!!!! ADMIN GETS & POSTS !!!!!!!!!!!!!!!!//
        //!!!!!!!!! ADMIN GETS & POSTS !!!!!!!!!!!!!!!!//
        //!!!!!!!!! ADMIN GETS & POSTS !!!!!!!!!!!!!!!!//
        //!!!!!!!!! ADMIN GETS & POSTS !!!!!!!!!!!!!!!!//
        //!!!!!!!!! ADMIN GETS & POSTS !!!!!!!!!!!!!!!!//



        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet("admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            return View();
        }





        //?            END OF WEBSITE FEATURES
        //?            END OF WEBSITE FEATURES
        //?            END OF WEBSITE FEATURES
        //?            END OF WEBSITE FEATURES
        //?            END OF WEBSITE FEATURES
        //!!!!!!!!!!!  POST FOR REGISTER AND LOGIN
        //!!!!!!!!!!!  POST FOR REGISTER AND LOGIN
        //!!!!!!!!!!!  POST FOR REGISTER AND LOGIN
        //!!!!!!!!!!!  POST FOR REGISTER AND LOGIN
        //!!!!!!!!!!!  POST FOR REGISTER AND LOGIN


        [HttpPost]
        [Route("submit")]
        public IActionResult Submit(User NewUser)
        {

            if (ModelState.IsValid)
            {
                if (dbContext.User.Any(u => u.Email == NewUser.Email))
                {

                    ModelState.AddModelError("Email", "Email already in use!");

                    return View("Admin");
                }

                // Initializing a PasswordHasher object, providing our User class as its
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                //Save your user object to the database
                dbContext.Add(NewUser);
                // OR dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                // Other code
                @ViewBag.Success = "You may now log in.";
                return View("Admin");
            }
            return View("Admin");
        }

        [HttpPost("log")]
        public IActionResult Log(LoginUser submission)
        {
            var userInDb = dbContext.User.FirstOrDefault(u => u.Email == submission.Email);

            if (submission.Password == null)
            {
                ModelState.AddModelError("Password", "Please populate Password Field.");
                return View("Index");
            }


            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                // If no user exists with provided email
                if (userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }

                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();

                // varify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submission, userInDb.Password, submission.Password);

                // result can be compared to 0 for failure
                if (result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
            }
            HttpContext.Session.SetInt32("LoggedUser", userInDb.UserID);
            return RedirectToAction("Dashboard");
        }

    }
}
