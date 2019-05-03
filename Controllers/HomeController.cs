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

        [HttpGet("inquirypage")]
        public IActionResult Inquiry()
        {
            return View();
        }

        [HttpGet("setnewappointment")]
        public IActionResult SetAppointment()
        {
            return RedirectToAction("appointmentpage");
        }

        [HttpGet("services")]
        public IActionResult Services()
        {
            return View("services");
        }

        [HttpGet("paymentoptions")]
        public IActionResult PaymentOptions()
        {
            return View();
        }


        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS
        //!!!!!!!!!!!  POSTS

        [HttpPost("createinquiry")]
        public IActionResult CreateInquiry(Inquiry newInquiry)
        {
            dbContext.Add(newInquiry);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }





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


        //!!!!!!!!! ADMIN GETS//!!!!!!!!! ADMIN GETS//!!!!!!!!! ADMIN GETS


        [HttpGet("admin")]
        public IActionResult Admin()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet("admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("Index");
            }

            var AllAppointments = dbContext.Appointment
            .OrderBy(d => d.StartDate)
            .ToList();

            var AllInquiries = dbContext.Inquiry
            .OrderBy(d => d.CreatedAt)
            .ToList();

            @ViewBag.AllAppointments = AllAppointments;
            @ViewBag.AllInquiries = AllInquiries;

            return View();
        }

        [HttpGet("admin/appointment/new")]
        public IActionResult New()
        {
            if (HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet("profile/{id}")]
        public IActionResult AppointmentProfile(int id)
        {
            if (HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("Index");
            }

            Appointment thisAppointment = dbContext.Appointment
            .SingleOrDefault(i => i.AppointmentID == id);

            return View(thisAppointment);
        }


        //!!!!!!!!! ADMIN POST//!!!!!!!!! ADMIN POST//!!!!!!!!! ADMIN POST


        [HttpPost]
        [Route("CreateAppt")]
        public IActionResult CreateAppointments(Appointment newAppointment)
        {
            string newApptDay = newAppointment.StartDate.ToString("dd");
            string newApptEnd = newAppointment.EndDate.ToString("dd");

            if (ModelState.IsValid)
            {
                DateTime CurrentTime = DateTime.Now;

                if (newAppointment.StartDate < CurrentTime)
                {
                    @ViewBag.Error = "Date must be in the future.";
                    return View("New");
                }


                if (newAppointment.EndDate < newAppointment.StartDate)
                {
                    @ViewBag.Error = "End Date must be after Start Date";
                    return View("New");
                }


                if (newApptDay != newApptEnd)
                {
                    @ViewBag.Error = "Appointments must be in the same day.";
                    return View("New");
                }


                dbContext.Add(newAppointment);
                dbContext.SaveChanges();

                int id = newAppointment.AppointmentID;
                string url = $"profile/{id}";

                return base.Redirect(url);
            }
            return View("New");
        }

        [HttpGet("admin/delete/{id}")]

        public IActionResult DeleteAppointment(int id)
        {
            Appointment retAppointment = dbContext.Appointment
            .SingleOrDefault(i => i.AppointmentID == id);

            dbContext.Remove(retAppointment);
            dbContext.SaveChanges();

            return RedirectToAction("AdminDashboard");
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
                return View("Admin");
            }


            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                // If no user exists with provided email
                if (userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Admin");
                }

                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();

                // varify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submission, userInDb.Password, submission.Password);

                // result can be compared to 0 for failure
                if (result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Admin");
                }
            }
            HttpContext.Session.SetInt32("LoggedUser", userInDb.UserID);
            return RedirectToAction("AdminDashboard");
        }

    }
}
