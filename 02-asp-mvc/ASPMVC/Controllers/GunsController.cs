using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPMVC.Models;
using ASPMVC.Data;
using Microsoft.AspNetCore.Authorization;

namespace ASPMVC.Controllers
{
    public class GunsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GunsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guns
        public async Task<IActionResult> Index(string gunType, string searchString)
        {
            var guns = from g in _context.Gun.Include(c => c.Manufacturer).AsNoTracking()
                select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                guns = guns.Where(s => s.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(gunType))
            {
                guns = guns.Where(x => x.Type == (GunType)int.Parse(gunType));
            }

            var gunTypeVM = new GunTypeViewModel
            {
                Guns = await guns.ToListAsync()
            };

            return View(gunTypeVM);
        }

        // GET: Guns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Gun
                .Include(s => s.Manufacturer)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (gun == null)
            {
                return NotFound();
            }

            return View(gun);
        }

        // GET: Guns/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            GunManufacturerDropDownList();
            return View();
        }

        // POST: Guns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ID,Name,ProductionDate,Type,Caliber,Price,ManufacturerID")] Gun gun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            GunManufacturerDropDownList(gun.ManufacturerID);
            return View(gun);
        }

        // GET: Guns/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Gun.FindAsync(id);
            if (gun == null)
            {
                return NotFound();
            }
            GunManufacturerDropDownList(gun.ManufacturerID);
            return View(gun);
        }

        // POST: Guns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ProductionDate,Type,Caliber,Price,ManufacturerID")] Gun gun)
        {
            if (id != gun.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GunExists(gun.ID))
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
            return View(gun);
        }

        // GET: Guns/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gun = await _context.Gun
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gun == null)
            {
                return NotFound();
            }

            return View(gun);
        }

        // POST: Guns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gun = await _context.Gun.FindAsync(id);
            _context.Gun.Remove(gun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GunExists(int id)
        {
            return _context.Gun.Any(e => e.ID == id);
        }

        // Manufacturer Dropdown List
        private void GunManufacturerDropDownList(object selectedManufacturer = null)
        {
            var manufacturersQuery = from m in _context.Manufacturer
                                   orderby m.Name
                                   select m;
            ViewBag.ManufacturerID = new SelectList(manufacturersQuery.AsNoTracking(), "ManufacturerID", "Name", selectedManufacturer);
        }
    }
}
