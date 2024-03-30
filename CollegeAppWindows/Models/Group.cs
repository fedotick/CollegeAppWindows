﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecialtyId { get; set; }
        public int Course {  get; set; }
        public int YearOfAdmission { get; set; }
    }
}
