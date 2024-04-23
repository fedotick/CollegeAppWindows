using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class GroupService
    {
        private Repository<Group> groupRepository = Repository<Group>.GetInstance;

        private static GroupService? instance;

        private GroupService() { }

        public static GroupService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GroupService();
                }

                return instance;
            }
        }

        public List<Group> GetAll()
        {
            return groupRepository.GetAll();
        }
    }
}
