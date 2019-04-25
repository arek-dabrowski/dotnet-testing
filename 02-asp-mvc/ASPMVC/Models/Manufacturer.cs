using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPMVC.Models
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [FirstCapital]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [FirstCapital]
        public string Country { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [FirstCapital]
        public string Headquarters { get; set; }

        [Display(Name = "Founded")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FoundDate { get; set; }

        [Display(Name = "Produced Guns")]
        public List<Gun> Guns { get; set; }
    }
}
