using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollegeAppWindows.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full name must be between 5 and 50 characters!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "IDNP is required!")]
        [StringLength(13, ErrorMessage = "IDNP must be 13 characters long!")]
        public string IDNP { get; set; }

        [Required(ErrorMessage = "Group is required!")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Subgroup number is required!")]
        [Range(1, 2, ErrorMessage = "Subgroup number must be between 1 and 2!")]
        public int SubgroupNumber { get; set; }

        [Required(ErrorMessage = "Card number is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Card number must be at least 1")]
        public int CardNumber { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public int StudentAddressId { get; set; }

        [RegularExpression(@"^\+373\d{8}$", ErrorMessage = "Phone number must start with +373 and contain 8 additional digits")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
