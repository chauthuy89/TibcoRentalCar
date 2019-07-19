using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TibcoCarRentals.Data;
using TibcoCarRentals.Models;

namespace TibcoCarRentals.Controllers
{
    public class BusBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BusBookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BusBookings
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.BusBooking.Include(c => c.Bus);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.BusBooking.Include(c => c.Bus).Where(b => b.UserID == _userManager.GetUserId(User)).Where(b => b.BookingConfirmed == true).Where(b => b.BookingCompleted == false);
                return View(await applicationDbContext.ToListAsync());
            }
            //var applicationDbContext = _context.BusBooking.Include(b => b.Bus);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: BusBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busBooking = await _context.BusBooking
                .Include(b => b.Bus)
                .FirstOrDefaultAsync(m => m.BusBookingID == id);
            if (busBooking == null)
            {
                return NotFound();
            }

            return View(busBooking);
        }

        // GET: BusBookings/Create
        public async Task<IActionResult> ConfirmBooking(int? id)
        {
            var busBooking = await _context.BusBooking.FindAsync(id);
            var bus = await _context.Bus.FindAsync(busBooking.BusID);
            bus.Quantity -= 1;
            //busBooking.BookingConfirmed = true;

            _context.Update(bus);
            _context.Update(busBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Booked(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busBooking = await _context.BusBooking
                .Include(c => c.Bus)
                .FirstOrDefaultAsync(m => m.BusBookingID == id);
            if (busBooking == null)
            {
                return NotFound();
            }

            return View(busBooking);
        }

        public async Task<IActionResult> Create(int id)
        {
            var bus = await _context.Bus.FindAsync(id);
            ViewData["BusID"] = id;
            ViewData["BusModel"] = bus.Model;
            ViewData["BusYear"] = bus.Year;
            ViewData["BusDailyPrice"] = bus.DailyPrice;
            ViewData["BusNumSeats"] = bus.NumSeats;
            ViewData["BusImage"] = bus.ImageLocation;
            return View();
        }

        // POST: BusBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusBookingID,NumOfGuest,DateToBook,SpecialRequirement,UserID,BusID")] BusBooking busBooking)
        {
            if (ModelState.IsValid)
            {
                busBooking.BookingConfirmed = false;
                _context.Add(busBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Booked), new { id = busBooking.BusBookingID });
            }
            ViewData["BusID"] = new SelectList(_context.Bus, "BusID", "BusID", busBooking.BusID);
            return View(busBooking);
        }

        // GET: BusBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busBooking = await _context.BusBooking.FindAsync(id);
            if (busBooking == null)
            {
                return NotFound();
            }
            ViewData["BusID"] = new SelectList(_context.Bus, "BusID", "BusID", busBooking.BusID);
            return View(busBooking);
        }

        // POST: BusBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusBookingID,NumOfGuest,DateToBook,SpecialRequirement,UserID,BusID")] BusBooking busBooking)
        {
            if (id != busBooking.BusBookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusBookingExists(busBooking.BusBookingID))
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
            ViewData["BusID"] = new SelectList(_context.Bus, "BusID", "BusID", busBooking.BusID);
            return View(busBooking);
        }

        // GET: BusBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busBooking = await _context.BusBooking
                .Include(b => b.Bus)
                .FirstOrDefaultAsync(m => m.BusBookingID == id);
            if (busBooking == null)
            {
                return NotFound();
            }

            return View(busBooking);
        }

        // POST: BusBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var busBooking = await _context.BusBooking.FindAsync(id);
            var bus = await _context.Car.FindAsync(busBooking.BusID);
            bus.Quantity += 1;

            _context.Update(bus);
            _context.BusBooking.Remove(busBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusBookingExists(int id)
        {
            return _context.BusBooking.Any(e => e.BusBookingID == id);
        }
    }
}
