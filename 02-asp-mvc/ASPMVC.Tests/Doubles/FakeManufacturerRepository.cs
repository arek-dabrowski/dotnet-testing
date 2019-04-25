using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASPMVC.Data.Repositories;
using ASPMVC.Models;

namespace ASPMVC.Tests.Doubles
{
    public class FakeManufacturerRepository : IManufacturerRepository
    {
        private List<Manufacturer> manufacturers = new List<Manufacturer>();

        public void Add(Manufacturer manufacturer)
        {
            manufacturers.Add(manufacturer);  
        }

        public void Delete(int manufacturerId)
        {
            Manufacturer manufacturerToDelete = manufacturers.FirstOrDefault(x => x.ManufacturerID == manufacturerId);
            manufacturers.Remove(manufacturerToDelete);
        }

        public IList<Manufacturer> GetAll()
        {
            return manufacturers;
        }

        public Manufacturer GetById(int manufacturerId)
        {
            return manufacturers
                .FirstOrDefault(x => x.ManufacturerID == manufacturerId);
        }

        public void Update(Manufacturer manufacturer)
        {
            var m = manufacturers.FirstOrDefault(x => x.ManufacturerID == manufacturer.ManufacturerID);
            m = manufacturer;
        }
    }
}
