using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASPMVC.Models;
using ASPMVC.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ASPMVC.Controllers
{
    public class GunsController : Controller
    {
        private readonly IGunRepository _gunRepo;
        private readonly IManufacturerRepository _manufacturerRepo;

        public GunsController(IGunRepository gunRepo, IManufacturerRepository manufacturerRepo)
        {
            _gunRepo = gunRepo;
            _manufacturerRepo = manufacturerRepo;
        }

        // GET: Guns
        public IActionResult Index(string gunType, string searchString)
        {
            var guns = _gunRepo.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                guns = guns.Where(s => s.Name.Contains(searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(gunType))
            {
                guns = guns.Where(x => x.Type == (GunType)int.Parse(gunType)).ToList();
            }

            var gunTypeVM = new GunTypeViewModel
            {
                Guns = guns.ToList()
            };

            return View(gunTypeVM);
        }

        // GET: Guns/Details/5
        public IActionResult Details(int id)
        {
            var gun = _gunRepo.GetById(id);
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
            ViewData["ManufacturerID"] = new SelectList(_manufacturerRepo.GetAll(), "ManufacturerID", "Name");
            return View();
        }

        // POST: Guns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([Bind("ID,Name,ProductionDate,Type,Caliber,Price,ManufacturerID")] Gun gun)
        {
            if (ModelState.IsValid)
            {
                _gunRepo.Add(gun);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ManufacturerID"] = new SelectList(_manufacturerRepo.GetAll(), "ManufacturerID", "Name", gun.ManufacturerID);
            return View(gun);
        }

        // GET: Guns/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            var gun = _gunRepo.GetById(id);
            if (gun == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerID"] = new SelectList(_manufacturerRepo.GetAll(), "ManufacturerID", "Name", gun.ManufacturerID);
            return View(gun);
        }

        // POST: Guns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id, [Bind("ID,Name,ProductionDate,Type,Caliber,Price,ManufacturerID")] Gun gun)
        {
            if (id != gun.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _gunRepo.Update(gun);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ManufacturerID"] = new SelectList(_manufacturerRepo.GetAll(), "ManufacturerID", "Name", gun.ManufacturerID);
            return View(gun);
        }

        // GET: Guns/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var gun = _gunRepo.GetById(id);
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
        public IActionResult DeleteConfirmed(int id)
        {
            _gunRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
