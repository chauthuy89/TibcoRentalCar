using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TibcoCarRentals.Data;
using TibcoCarRentals.Models;

namespace TibcoCarRentals.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;

        public CarsController(ApplicationDbContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;

        }

        // GET: Cars
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
            cars = cars.Where(x => x.Quantity > 0);
            var carModelVM = new CarModelViewModel
            {
                Models = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Cars = await cars.ToListAsync()
            };

            return View(carModelVM);
        }

        public async Task<IActionResult> ClearSearch()
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Roles ="Admin")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarID,Model,Year,DailyPrice,NumSeats,Warrent,Registration,ImageLocation")] Car car, IFormFile picture)
        {
            car.ImageLocation = "/images/car/unavailable.jpg";
            if (picture != null)
            {
                car.ImageLocation = "/images/car/" + Path.GetFileName(picture.FileName);
                var fileName = Path.Combine(he.WebRootPath + "/images/car/", Path.GetFileName(picture.FileName));
                picture.CopyTo(new FileStream(fileName, FileMode.Create));
                //ViewData["fileLocation"] = "/images/event/" + Path.GetFileName(pic.FileName);
            }
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            if (car.ImageLocation != null)
            {
                ViewData["fileLocation"] = car.ImageLocation;
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarID,Model,Year,DailyPrice,NumSeats,Warrent,Registration,ImageLocation")] Car car, IFormFile picture)
        {
            if (picture != null)
            {
                car.ImageLocation = "/images/car/" + Path.GetFileName(picture.FileName);
                var fileName = Path.Combine(he.WebRootPath + "/images/car/", Path.GetFileName(picture.FileName));
                picture.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["fileLocation"] = "/images/car/" + Path.GetFileName(picture.FileName);
            }
            if (id != car.CarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarID == id);
        }
    }
}
