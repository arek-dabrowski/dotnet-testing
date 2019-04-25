using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPMVC.Models
{
    public class ProducedGunsViewModel
    {
        public Manufacturer Manufacturer { get; set; }

        [Display(Name = "Produced Guns")]
        public List<Gun> Guns { get; set; }
    }
}
