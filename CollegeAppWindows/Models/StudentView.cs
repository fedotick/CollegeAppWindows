using System;

namespace CollegeAppWindows.Models
{
    public class StudentView
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? IDNP { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public byte? SubgroupNumber { get; set; }
        public short? CardNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int StudentAddressId { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public short? ApartmentNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
