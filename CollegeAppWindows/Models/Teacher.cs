using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeAppWindows.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full name must be between 5 and 50 characters!")]
        public string? FullName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Cathedra is required!")]
        public int CathedraId { get; set; }

        [Required(ErrorMessage = "Experience is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Experience cannot be negative!")]
        public byte? Experience { get; set; }

        [Required(ErrorMessage = "Date of birth is required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public int TeacherAddressId { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [RegularExpression(@"^\+373\d{8}$", ErrorMessage = "Phone number must start with +373 and contain 8 additional digits!")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        public string? Email { get; set; }
    }
}
