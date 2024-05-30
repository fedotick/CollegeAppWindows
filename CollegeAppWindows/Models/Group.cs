using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeAppWindows.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(7, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 7 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialty is required!")]
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Range(1, 4, ErrorMessage = "Course must be between 1 and 4 characters!")]
        public int Course {  get; set; }

        [Required(ErrorMessage = "Year of admission is required!")]
        public int YearOfAdmission { get; set; }

        public int TeacherId { get; set;}
    }
}
