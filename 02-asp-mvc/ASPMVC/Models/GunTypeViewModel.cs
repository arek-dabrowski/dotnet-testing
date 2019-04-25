using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ASPMVC.Models
{
    public class GunTypeViewModel
    {
        public List<Gun> Guns;
        public SelectList Types;
        public string GunType { get; set; }
        public string SearchString { get; set; }
    }
}