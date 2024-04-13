using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System;
using System.Windows;

namespace CollegeAppWindows.Services
{
    internal class TeacherService
    {
        private Repository<Teacher> teacherRepository;
        private Repository<TeacherAddress> teacherAddressRepository;

        public TeacherService() 
        {
            teacherRepository = new Repository<Teacher>();
            teacherAddressRepository = new Repository<TeacherAddress>();
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
