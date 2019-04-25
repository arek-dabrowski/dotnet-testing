using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASPMVC.Data.Repositories;
using ASPMVC.Models;

namespace ASPMVC.Tests.Doubles
{
    public class FakeGunRepository : IGunRepository
    {
        private List<Gun> guns = new List<Gun>();

        public void Add(Gun gun)
        {
            guns.Add(gun);
        }

        public void Delete(int gunId)
        {
            Gun gunToDelete = guns.FirstOrDefault(x => x.ID == gunId);
            guns.Remove(gunToDelete);
        }

        public IList<Gun> GetAll()
        {
            return guns;
        }

        public IList<Gun> GetAllManufacturerGuns(int manufacturerId)
        {
            return guns
                .Where(x => x.ManufacturerID == manufacturerId)
                .ToList();
        }

        public Gun GetById(int gunId)
        {
            return guns
                .FirstOrDefault(x => x.ID == gunId);
        }

        public void Update(Gun gun)
        {
            var g = guns.FirstOrDefault(x => x.ID == gun.ID);
            g = gun;
        }
    }
}
