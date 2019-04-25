using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASPMVC.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Manufacturer.Any())
                {
                    context.Manufacturer.AddRange(
                        new Manufacturer {Name = "H&K", Country = "Germany", Headquarters = "Germany", FoundDate = DateTime.Parse("1989-2-12")},
                        new Manufacturer {Name = "Colt", Country = "USA", Headquarters = "USA", FoundDate = DateTime.Parse("1989-2-12")},
                        new Manufacturer {Name = "XDCMP", Country = "Poland", Headquarters = "Poland", FoundDate = DateTime.Parse("1989-2-12")}
                    );
                    context.SaveChanges();
                }

                if (!context.Gun.Any())
                {
                    context.Gun.AddRange(
                        new Gun {Name = "MP5", ProductionDate = DateTime.Parse("1989-2-12"), Type = GunType.Assault,Caliber = "9mm", Price = 1999.99M, ManufacturerID = 1},
                        new Gun {Name = "AK-47", ProductionDate = DateTime.Parse("1989-2-12"), Type = GunType.Assault,Caliber = "9mm", Price = 1999.99M, ManufacturerID = 2},
                        new Gun {Name = "M1911", ProductionDate = DateTime.Parse("1989-2-12"), Type = GunType.Pistol,Caliber = "9mm", Price = 1999.99M, ManufacturerID = 3},
                        new Gun {Name = "M82", ProductionDate = DateTime.Parse("1989-2-12"), Type = GunType.Sniper, Caliber = "9mm", Price = 1999.99M, ManufacturerID = 1}
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
