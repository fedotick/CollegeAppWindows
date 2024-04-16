using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class TeacherViewService
    {
        private Repository<TeacherView> teacherViewRepository = Repository<TeacherView>.GetInstance;

        private static TeacherViewService? instance;

        private TeacherViewService() { }

        public static TeacherViewService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TeacherViewService();
                }

                return instance;
            }
        }

        public List<TeacherView> GetAll()
        {
            return teacherViewRepository.GetAll();
        }
    }
}
