using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class StudentAddress
    {
        public int Id { get; set; }
	    public string Region { get; set; }
        public string City { get; set; }
	    public string Street { get; set; }
        public string HouseNumber { get; set; }
	    public int ApartmentNumber { get; set; }
    }
}
