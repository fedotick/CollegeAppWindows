using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Data.SqlClient;
using System.Windows;
using System;
using CollegeAppWindows.Utilities;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class StudentService
    {
        private Repository<Student> studentRepository = Repository<Student>.GetInstance;
        private Repository<StudentAddress> studentAddressRepository = Repository<StudentAddress>.GetInstance;

        private static StudentService? instance;

        private StudentService() { }

        public static StudentService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentService();
                }

                return instance;
            }
        }

        public void Add(Student student, StudentAddress studentAddress)
        {
            if (!isValidate(student)) return;
            if (!isValidate(studentAddress)) return;

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int studentAddressId = studentAddressRepository.Add(studentAddress, transaction);
                    student.StudentAddressId = studentAddressId;
                    studentRepository.Add(student, transaction);

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

        public void Update(Student student, StudentAddress studentAddress)
        {
            if (!isValidate(student)) return;
            if (!isValidate(studentAddress)) return;

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    studentAddressRepository.Update(studentAddress, transaction);
                    studentRepository.Update(student, transaction);

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

        public void Delete(int id)
        {
            Student student = studentRepository.GetById(id);

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    studentRepository.DeleteById(id, transaction);
                    studentAddressRepository.DeleteById(student.StudentAddressId, transaction);

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

        public bool isValidate<T>(T model)
        {
            Dictionary<string, List<string>> errors = ValidationUtil.ValidateModel(model);

            if (errors != null && errors.Count > 0)
            {
                foreach (var errorEntry in errors)
                {
                    string propertyName = errorEntry.Key;
                    List<string> errorMessages = errorEntry.Value;

                    foreach (string errorMessage in errorMessages)
                    {
                        MessageBox.Show($"- {errorMessage}", propertyName);
                    }
                }

                return false;
            }

            return true;
        }
    }
}
