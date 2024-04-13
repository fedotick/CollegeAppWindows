using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class TeacherViewService
    {
        private Repository<TeacherView> teacherViewRepository = new Repository<TeacherView>();
        public TeacherViewService() { }

        public List<TeacherView> GetAll()
        {
            return teacherViewRepository.GetAll();
        }
    }
}
