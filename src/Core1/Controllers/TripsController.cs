using Core1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Core1.ViewModels;

namespace Core1.Controllers
{
    public class TripsController : Controller
    {
        private Models.Core1Context _context;
        private UserManager<CoreUser> _manager;
        private SignInManager<CoreUser> _smanager;

        public TripsController(Models.Core1Context context, UserManager<CoreUser> manager, SignInManager<CoreUser> smanager)
        {
            _context = context;
            _manager = manager;
            _smanager = smanager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyTrips()
        {
            var data = _context.GetMyTripStop(User.Identity.Name);
            return View(data);
        }

        
        public IActionResult Explore()
        {
            var data = _context.GetTrips();
            return View(data);
        }


        
        [HttpGet("trips/trip/{tripName}/{id}")]
        public IActionResult Trip(string tripName, string id)
        {
            var data = _context.GetTripByName(tripName, id);
            return View(data);
        }

        //use this method to initialize or 'seed' the database with sample data
        //todo maybe await/async, maybe remove wait
        public IActionResult CreateTrip()
        {
            /*
            var entry = new Trip()
            {
                Name = "US Trip",
                DateCreated = DateTime.Now,
                UserName = "babanjure",
                Stops = new List<Stop>()
                    {
                        new Stop() {  Name = "Atlanta, GA", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 0 },
                        new Stop() {  Name = "New York, NY", Arrival = new DateTime(2014, 6, 9), Latitude = 40.712784, Longitude = -74.005941, Order = 1 },
                        new Stop() {  Name = "Boston, MA", Arrival = new DateTime(2014, 7, 1), Latitude = 42.360082, Longitude = -71.058880, Order = 2 },
                        new Stop() {  Name = "Chicago, IL", Arrival = new DateTime(2014, 7, 10), Latitude = 41.878114, Longitude = -87.629798, Order = 3 },
                        new Stop() {  Name = "Seattle, WA", Arrival = new DateTime(2014, 8, 13), Latitude = 47.606209, Longitude = -122.332071, Order = 4 },
                        new Stop() {  Name = "Atlanta, GA", Arrival = new DateTime(2014, 8, 23), Latitude = 33.748995, Longitude = -84.387982, Order = 5 },
                    }
            };*/

            var entry = new Trip()
            {
                Name = "Zlatni rat - Brac",
                DateCreated = DateTime.Now,
                UserName = "babanjure",
                Descripton = "Zlatni rat je vjerojatno najpoznatija plaza na Jadranu, smjestena u Bolu",
                Stops = JsonConvert.SerializeObject(new List<Stop>()
                    {
                        new Stop() {  Latitude = 33.748995, Longitude = -84.387982, Order = 0 },
                        new Stop() {  Latitude = 40.712784, Longitude = -74.005941, Order = 1 },
                        new Stop() {  Latitude = 42.360082, Longitude = -71.058880, Order = 2 },
                        new Stop() {  Latitude = 41.878114, Longitude = -87.629798, Order = 3 }
                    })
                };
            
            _context.Trips.Add(entry);
            _context.SaveChanges();
            Debug.WriteLine("Added trip");
            return Ok();
        }

        public IActionResult CreateUser()
        {
            if (_manager.FindByEmailAsync("baban.jure@gmail.com") == null)
            {
                var user = new CoreUser()
                {
                    UserName = "babanjure",
                    Email = "baban.jure@gmail.com"
                };

                _manager.CreateAsync(user, "P@ssw0rd!");
                _context.SaveChanges();
            }
            return Ok();
        }

        /*
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "Trips");
            }

            return View("Index");
        }*/

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _smanager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Explore", "Trips");
                }
                else
                {
                    ModelState.AddModelError("", "username or password incorrect");
                }
            }

            return View("Index");
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _smanager.SignOutAsync();
            }

            return RedirectToAction("Index", "Trips");
        }
    }
}
