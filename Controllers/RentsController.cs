using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Controllers
{
    [Authorize]
    public class RentsController : Controller
    {
        private readonly Car_Rental_DBContext _context;

        public RentsController(Car_Rental_DBContext context)
        {
            _context = context;
        }

        // GET: Rents
        public async Task<IActionResult> Index()
        {
            var rents = _context.Cars.Where(r => r.Available == true);
            List<Cars> c = new List<Cars>();
            foreach (var r in rents)
            {
                c.Add(_context.Cars.Where(ca => ca.IdCar == r.IdCar).FirstOrDefault());
            }
            return View(c);
        }

        // GET: Rents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = HttpContext.User.Identity.Name;
            var cars = await _context.Cars
                .SingleOrDefaultAsync(m => m.IdCar == id);
            if (cars == null)
            {
                return NotFound();
            }
            cars.Available = false;
            Rent s = new Rent {Login=user, IdCar=id };
            _context.Update(cars);
            await _context.SaveChangesAsync();
            _context.Add(s);
            await _context.SaveChangesAsync();

            var cars2 = await _context.Cars
                .SingleOrDefaultAsync(m => m.IdCar == id);
            return View(cars2);
        }

        // GET: Rents/Create
        public IActionResult Create(string id)
        {
            
            var user = HttpContext.User.Identity.Name;
            ViewData["IdCar"] = id;
            ViewData["Login"] = user;
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRent,IdCar,Login")] Rent rent)
        {
            var user = HttpContext.User.Identity.Name;
            rent.Login = user;
            if (ModelState.IsValid)
            {
                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCar"] = new SelectList(_context.Cars, "IdCar", "IdCar", rent.IdCar);
            ViewData["Login"] = new SelectList(_context.Users, "Login", "Login", rent.Login);
            return View(rent);
        }

        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent.SingleOrDefaultAsync(m => m.IdRent == id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["IdCar"] = new SelectList(_context.Cars, "IdCar", "IdCar", rent.IdCar);
            ViewData["Login"] = new SelectList(_context.Users, "Login", "Login", rent.Login);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRent,IdCar,Login")] Rent rent)
        {
            if (id != rent.IdRent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.IdRent))
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
            ViewData["IdCar"] = new SelectList(_context.Cars, "IdCar", "IdCar", rent.IdCar);
            ViewData["Login"] = new SelectList(_context.Users, "Login", "Login", rent.Login);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .Include(r => r.IdCarNavigation)
                .Include(r => r.LoginNavigation)
                .SingleOrDefaultAsync(m => m.IdRent == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rent = await _context.Rent.SingleOrDefaultAsync(m => m.IdRent == id);
            _context.Rent.Remove(rent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _context.Rent.Any(e => e.IdRent == id);
        }
    }
}
