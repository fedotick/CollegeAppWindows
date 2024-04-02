using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class TeacherAddress
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Region is required!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Region must be between 5 and 30 characters!")]
        public string Region { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "City must be between 5 and 30 characters!")]
        public string City { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Street must be between 5 and 50 characters!")]
        public string Street { get; set; }

        [StringLength(5, MinimumLength = 1, ErrorMessage = "House number must be between 1 and 5 characters!")]
        public string HouseNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Apartmen number must be at least 1")]
        public int ApartmentNumber { get; set; }
    }
}
