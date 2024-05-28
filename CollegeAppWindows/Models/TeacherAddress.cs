using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeAppWindows.Models
{
    public class TeacherAddress
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Region is required!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Region must be between 5 and 30 characters!")]
        public string? Region { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "City must be between 5 and 30 characters!")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Street is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Street must be between 5 and 50 characters!")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "House number is required!")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "House number must be between 1 and 5 characters!")]
        public string? HouseNumber { get; set; }

        [Required(ErrorMessage = "Apartment number is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Apartmen number must be at least 1")]
        public short? ApartmentNumber { get; set; }
    }
}
