using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class UsersCarsController : Controller
    {
        private readonly Car_Rental_DBContext _context;

        public UsersCarsController(Car_Rental_DBContext context)
        {
            _context = context;
        }

        // GET: UsersCars
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User.Identity.Name;
            var rents = _context.Rent.Where(r => r.Login == user);
            List<Cars> c = new List<Cars>();
            foreach(var r in rents)
            {
                c.Add(_context.Cars.Where(ca => ca.IdCar == r.IdCar).FirstOrDefault());
            }
            
            return View(c);
                
                
        }

        // GET: UsersCars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cars = await _context.Cars
                .SingleOrDefaultAsync(m => m.IdCar == id);
            if (cars == null)
            {
                return NotFound();
            }

            return View(cars);
        }

        // GET: UsersCars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersCars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCar,Mark,Model,Category,Available")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cars);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cars);
        }

        // GET: UsersCars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cars = await _context.Cars.SingleOrDefaultAsync(m => m.IdCar == id);
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // POST: UsersCars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCar,Mark,Model,Category,Available")] Cars cars)
        {
            if (id != cars.IdCar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cars);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarsExists(cars.IdCar))
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
            return View(cars);
        }

        // GET: UsersCars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cars = await _context.Cars
                .SingleOrDefaultAsync(m => m.IdCar == id);
            if (cars == null)
            {
                return NotFound();
            }

            return View(cars);
        }

        // POST: UsersCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cars = await _context.Cars.SingleOrDefaultAsync(m => m.IdCar == id);
            _context.Cars.Remove(cars);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarsExists(int id)
        {
            return _context.Cars.Any(e => e.IdCar == id);
        }
    }
}
