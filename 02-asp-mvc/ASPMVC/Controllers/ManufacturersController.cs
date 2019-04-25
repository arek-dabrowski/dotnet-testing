using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ASPMVC.Models;
using ASPMVC.Data.Repositories;

using Microsoft.AspNetCore.Authorization;

namespace ASPMVC.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly IGunRepository _gunRepo;
        private readonly IManufacturerRepository _manufacturerRepo;

        public ManufacturersController(IGunRepository gunRepo, IManufacturerRepository manufacturerRepo)
        {
            _gunRepo = gunRepo;
            _manufacturerRepo = manufacturerRepo;
        }


        // GET: Manufacturers
        public IActionResult Index()
        {
            var manufacturers = _manufacturerRepo.GetAll();
            return View(manufacturers);
        }

        // GET: Manufacturers/Details/5
        public IActionResult Details(int id)
        {
            ProducedGunsViewModel model = new ProducedGunsViewModel();

            var manufacturer = _manufacturerRepo.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            model.Manufacturer = manufacturer;

            var guns = _gunRepo.GetAllManufacturerGuns(id);

            List<Gun> GunList = new List<Gun>();

            foreach (var item in guns)
            {
                GunList.Add(item);
            }
            model.Guns = GunList;
            return View(model);
        }

        // GET: Manufacturers/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([Bind("ManufacturerID,Name,Country,Headquarters,FoundDate")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _manufacturerRepo.Add(manufacturer);
                return RedirectToAction(nameof(Index));
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            var manufacturer = _manufacturerRepo.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id, [Bind("ManufacturerID,Name,Country,Headquarters,FoundDate")] Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _manufacturerRepo.Update(manufacturer);
                return RedirectToAction(nameof(Index));
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var manufacturer = _manufacturerRepo.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
        {
            _manufacturerRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
