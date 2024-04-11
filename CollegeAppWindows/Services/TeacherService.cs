using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using CollegeAppWindows.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;

namespace CollegeAppWindows.Services
{
    internal class TeacherService
    {
        private Repository<Teacher> teacherRepository;
        private Repository<TeacherAddress> teacherAddressRepository;

        public TeacherService(SqlConnection connection) 
        {
            teacherRepository = new Repository<Teacher>(connection);
            teacherAddressRepository = new Repository<TeacherAddress>(connection);
        }

        public void Add(Teacher teacher, TeacherAddress teacherAddress)
        {
            try
            {
                int teacherAddressId = teacherAddressRepository.Add(teacherAddress);
                teacher.TeacherAddressId = teacherAddressId;
                teacherRepository.Add(teacher);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
