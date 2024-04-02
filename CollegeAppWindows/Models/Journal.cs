using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows.Models
{
    public class Journal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject is required!")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Student is required!")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Teacher is required!")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Grade is required!")]
        // Добавить проверку на оценку
        public string GradeGap { get; set; }
    }
}
