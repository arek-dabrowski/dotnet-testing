using System.Collections.Generic;
using ASPMVC.Models;

namespace ASPMVC.Data.Repositories
{
    public interface IGunRepository
    {
        IList<Gun> GetAll();
        IList<Gun> GetAllManufacturerGuns(int manufacturerId);

        Gun GetById(int gunId);
        void Add(Gun gun);
        void Update(Gun gun);
        void Delete(int gunId);
    }
}
