using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System;
using System.Data.SqlClient;
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
            SqlConnection connection = DataBase.Instance.GetConnection();

            DataBase.Instance.OpenConnection();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int teacherAddressId = teacherAddressRepository.Add(teacherAddress, transaction);
                    teacher.TeacherAddressId = teacherAddressId;
                    teacherRepository.Add(teacher, transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    MessageBox.Show(ex.Message);
                }
            }

            DataBase.Instance.CloseConnection();
        }
    }
}
