using System;

namespace CollegeAppWindows.Models
{
    public class TeacherView
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int CathedraId { get; set; }
        public string? CathedraName { get; set; }
        public byte? Experience { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int TeacherAddressId { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public short? ApartmentNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
