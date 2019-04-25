using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ASPMVC.Models;

namespace ASPMVC.Data.Repositories
{
    public class GunRepository : IGunRepository
    {
        private readonly ApplicationDbContext _context;
        public GunRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Gun gun)
        {
            _context.Gun.Add(gun);
            _context.SaveChanges();
        }

        public void Delete(int gunId)
        {
            var gun = _context.Gun.FirstOrDefault(x => x.ID == gunId);
            if (gun != null)
            {
                _context.Gun.Remove(gun);
                _context.SaveChanges();
            }
        }

        public IList<Gun> GetAll()
        {
            return _context.Gun
                .Include(x => x.Manufacturer)
                .ToList();
        }

        public Gun GetById(int gunId)
        {
            return _context.Gun
                .Include(x => x.Manufacturer)
                .FirstOrDefault(x => x.ID == gunId);
        }

        public IList<Gun> GetAllManufacturerGuns(int manufacturerId)
        {
            return _context.Gun
                .Include(x => x.Manufacturer)
                .Where(x => x.ManufacturerID == manufacturerId)
                .ToList();
        }

        public void Update(Gun gun)
        {
            _context.Gun.Update(gun);
            _context.SaveChanges();
        }
    }
}
