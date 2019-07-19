using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TibcoCarRentals.Data;
using TibcoCarRentals.Models;

namespace TibcoCarRentals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string carModel, string searchYear, string searchPrice, string searchSeats)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Car
                                            orderby m.Model
                                            select m.Model;

            var cars = from m in _context.Car
                       select m;

            if (!string.IsNullOrEmpty(searchYear))
            {
                cars = cars.Where(s => s.Year.Equals(int.Parse(searchYear)));
            }

            if (!string.IsNullOrEmpty(searchPrice))
            {
                cars = cars.Where(s => s.DailyPrice.Equals(int.Parse(searchPrice)));
            }

            if (!string.IsNullOrEmpty(searchSeats))
            {
                cars = cars.Where(s => s.NumSeats.Equals(int.Parse(searchSeats)));
            }

            if (!string.IsNullOrEmpty(carModel))
            {
                cars = cars.Where(x => x.Model == carModel);
            }

            var carModelVM = new MainViewModel
            {
                Models = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Cars = await cars.ToListAsync(),
                Buses = await _context.Bus.ToListAsync()
            };
            //var buses = await _context.BusType.ToListAsync();
            return View(carModelVM);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
