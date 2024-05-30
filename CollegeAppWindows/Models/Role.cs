using System.ComponentModel.DataAnnotations;

namespace CollegeAppWindows.Models
{
    internal class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters!")]
        public string? Name { get; set; }
    }
}
