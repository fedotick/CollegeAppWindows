using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace CollegeAppWindows.Services
{
    internal class TeacherService
    {
        private Repository<Teacher> teacherRepository = Repository<Teacher>.GetInstance;
        private Repository<TeacherAddress> teacherAddressRepository = Repository<TeacherAddress>.GetInstance;

        private static TeacherService? instance;

        private TeacherService() { }

        public static TeacherService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TeacherService();
                }

                return instance;
            }
        }

        public void Add(Teacher teacher, TeacherAddress teacherAddress)
        {
            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

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

            DataBase.GetInstance.CloseConnection();
        }

        public void Update(Teacher teacher, TeacherAddress teacherAddress)
        {
            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    teacherAddressRepository.Update(teacherAddress, transaction);
                    teacherRepository.Update(teacher, transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    MessageBox.Show(ex.Message);
                }
            }

            DataBase.GetInstance.CloseConnection();
        }
    }
}
