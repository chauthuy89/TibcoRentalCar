using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TibcoCarRentals.Data;
using TibcoCarRentals.Models;

namespace TibcoCarRentals.Controllers
{
    public class BusesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index(string busModel, string searchYear, string searchPrice, string searchSeats)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Bus
                                            orderby m.Model
                                            select m.Model;

            var buses = from m in _context.Bus
                       select m;
            buses = buses.Where(x => x.Quantity > 0);
            if (!string.IsNullOrEmpty(searchYear))
            {
                buses = buses.Where(s => s.Year.Equals(int.Parse(searchYear)));
            }

            if (!string.IsNullOrEmpty(searchPrice))
            {
                buses = buses.Where(s => s.DailyPrice.Equals(int.Parse(searchPrice)));
            }

            if (!string.IsNullOrEmpty(searchSeats))
            {
                buses = buses.Where(s => s.NumSeats.Equals(int.Parse(searchSeats)));
            }

            if (!string.IsNullOrEmpty(busModel))
            {
                buses = buses.Where(x => x.Model == busModel);
            }

            var busModelVM = new BusViewModel
            {
                Models = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Buses = await buses.ToListAsync()
            };

            return View(busModelVM);
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .FirstOrDefaultAsync(m => m.BusID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusID,Year,DailyPrice,Model,NumSeats,Warrent,Registration,ImageLocation")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // GET: Buses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusID,Year,DailyPrice,Model,NumSeats,Warrent,Registration,ImageLocation")] Bus bus)
        {
            if (id != bus.BusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.BusID))
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
            return View(bus);
        }

        // GET: Buses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .FirstOrDefaultAsync(m => m.BusID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bus = await _context.Bus.FindAsync(id);
            _context.Bus.Remove(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
            return _context.Bus.Any(e => e.BusID == id);
        }
    }
}
