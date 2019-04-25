using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ASPMVC.Models;

namespace ASPMVC.Data.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext _context;
        public ManufacturerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Manufacturer manufacturer)
        {
            _context.Manufacturer.Add(manufacturer);
            _context.SaveChanges();
        }

        public void Delete(int manufacturerId)
        {
            var manufacturer = _context.Manufacturer.FirstOrDefault(x => x.ManufacturerID == manufacturerId);
            if (manufacturer != null)
            {
                _context.Manufacturer.Remove(manufacturer);
                _context.SaveChanges();
            }
        }

        public IList<Manufacturer> GetAll()
        {
            return _context.Manufacturer.ToList();
        }

        public Manufacturer GetById(int manufacturerId)
        {
            return _context.Manufacturer.FirstOrDefault(x => x.ManufacturerID == manufacturerId);
        }

        public void Update(Manufacturer manufacturer)
        {
            _context.Manufacturer.Update(manufacturer);
            _context.SaveChanges();
        }
    }
}
