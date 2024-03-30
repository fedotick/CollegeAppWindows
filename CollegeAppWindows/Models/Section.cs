using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Menager { get; set; }
        public int SpecialtyId { get; set; }
    }
}
