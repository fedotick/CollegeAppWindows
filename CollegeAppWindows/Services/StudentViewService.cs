using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class StudentViewService
    {
        private Repository<StudentView> studentViewRepository = Repository<StudentView>.GetInstance;

        private static StudentViewService? instance;

        private StudentViewService() { }

        public static StudentViewService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentViewService();
                }

                return instance;
            }
        }

        public List<StudentView> GetAll()
        {
            return studentViewRepository.GetAll();
        }
    }
}
