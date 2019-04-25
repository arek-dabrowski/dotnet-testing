using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPMVC.Models
{
    public class Gun
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [FirstCapital]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductionDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a Gun Type")]
        [DisplayFormat(NullDisplayText = "Type not set")]
        public GunType Type { get; set; }

        public string Caliber { get; set; }

        [Range(0, 9999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }

    public enum GunType
    {
        None,
        Assault,
        Pistol,
        Revolver,
        Shotgun,
        Sniper
    }

}
