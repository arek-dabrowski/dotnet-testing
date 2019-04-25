using System.Collections.Generic;
using ASPMVC.Models;

namespace ASPMVC.Data.Repositories
{
    public interface IManufacturerRepository
    {
        IList<Manufacturer> GetAll();
        Manufacturer GetById(int manufacturerId);
        void Add(Manufacturer manufacturer);
        void Update(Manufacturer manufacturer);
        void Delete(int manufacturerId);
    }
}
