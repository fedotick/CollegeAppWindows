using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollegeAppWindows.Models
{
    public class Teacher
    {
        public int Id { get; set; }
	    public string FullName { get; set; }
        public int CathedraId { get; set; }
        public int Experience { get; set; }
	    public DateTime DateOfBirth { get; set; }
        public int AddressId { get; set; }
        public string PhoneNumber { get; set; }
	    public string Email { get; set; }
    }
}
