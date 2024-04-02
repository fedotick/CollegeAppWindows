using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class Section
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manager is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Manager must be between 5 and 50 characters!")]
        public string Manager { get; set; }

        [Required(ErrorMessage = "Specialty is required!")]
        public int SpecialtyId { get; set; }
    }
}
