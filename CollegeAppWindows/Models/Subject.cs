using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Semester is required!")]
        [Range(1, 2, ErrorMessage = "Semester must be 1 or 2!")]
        public int Semester { get; set; }

        [Required(ErrorMessage = "Number of hours is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of hours must be greater than 0")]
        public int NumberOfHours { get; set; }
    }
}
