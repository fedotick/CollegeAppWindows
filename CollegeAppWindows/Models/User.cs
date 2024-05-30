using System.ComponentModel.DataAnnotations;

namespace CollegeAppWindows.Models
{
    internal class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters!")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 255 characters!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is required!")]
        public int? RoleId { get; set; }
        public int? TeacherId { get; set; }
    }
}
