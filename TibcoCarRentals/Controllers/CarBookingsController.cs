using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TibcoCarRentals.Data;
using TibcoCarRentals.Models;

namespace TibcoCarRentals.Controllers
{
    public class CarBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CarBookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CarBookings
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.CarBooking.Include(c => c.Car);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.CarBooking.Include(c => c.Car).Where(b => b.UserID == _userManager.GetUserId(User)).Where(b => b.BookingConfirmed == true).Where(b => b.BookingCompleted == false);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: CarBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carBooking = await _context.CarBooking
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (carBooking == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            return View(carBooking);
        }

        public async Task<IActionResult> ConfirmBooking(int? id)
        {
            var carBooking = await _context.CarBooking.FindAsync(id);
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            var car = await _context.Car.FindAsync(carBooking.CarID);
            car.Quantity -= 1;
            carBooking.BookingConfirmed = true;

            _context.Update(car);
            _context.Update(carBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Booked(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carBooking = await _context.CarBooking
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (carBooking == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            DateTime date1 = carBooking.StartDate;
            DateTime date2 = carBooking.FinishDate;
            int days = ((TimeSpan)(date2 - date1)).Days + 1;
            ViewData["DaysTaken"] = days;
            ViewData["CarDailyPrice"] = days * carBooking.Car.DailyPrice;
            return View(carBooking);
        }

        // GET: CarBookings/Create
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarID"] = id;
            ViewData["CarModel"] = car.Model;
            ViewData["CarYear"] = car.Year;
            ViewData["CarDailyPrice"] = car.DailyPrice;
            ViewData["CarNumSeats"] = car.NumSeats;
            ViewData["CarImage"] = car.ImageLocation;
            return View();
        }

        // POST: CarBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,StartDate,FinishDate,DaysTaken,UserID,CarID,TotalPrice")] CarBooking carBooking)
        {
            if (ModelState.IsValid)
            {
                //DateTime date1 = carBooking.StartDate;
                //DateTime date2 = carBooking.FinishDate;
                //int daysDiff = ((TimeSpan)(date2 - date1)).Days;
                //ViewData["DaysTaken"] = daysDiff;

                carBooking.BookingConfirmed = false;
                _context.Add(carBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Booked), new { id = carBooking.BookingID });
            }
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "CarID", carBooking.CarID);
            //return View(carBooking, carBooking.CarID));
            return RedirectToAction(nameof(Create), new { id = carBooking.CarID });
        }

        // GET: CarBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carBooking = await _context.CarBooking.FindAsync(id);

            if (carBooking == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            ViewData["CarID"] = carBooking.CarID;
            return View(carBooking);
        }

        // POST: CarBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,StartDate,FinishDate,DaysTaken,UserID,CarID,TotalPrice")] CarBooking carBooking)
        {

            if (id != carBooking.BookingID)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    carBooking.BookingConfirmed = true;
                    _context.Update(carBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarBookingExists(carBooking.BookingID))
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
            ViewData["CarID"] = carBooking.CarID;
            return View(carBooking);
        }

        // GET: CarBookings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var carBooking = await _context.CarBooking
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (carBooking == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            return View(carBooking);
        }

        // POST: CarBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carBooking = await _context.CarBooking.FindAsync(id);
            if (_userManager.GetUserId(User) != carBooking.UserID && !User.IsInRole("Admin"))
            {
                return new RedirectResult("/Identity/Account/AccessDenied");
            }
            var car = await _context.Car.FindAsync(carBooking.CarID);
            car.Quantity += 1;

            _context.Update(car);
            _context.CarBooking.Remove(carBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarBookingExists(int id)
        {
            return _context.CarBooking.Any(e => e.BookingID == id);
        }
    }
}
