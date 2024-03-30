using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollegeAppWindows.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IDNP { get; set; }
        public int GroupId { get; set; }
	    public int SubgroupNumber { get; set; }
        public int CardNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int StudentAddressId { get; set; }
        public string PhoneNumber { get; set; }
	    public string Email { get; set; }
    }
}
