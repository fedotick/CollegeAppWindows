using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    class RoleService
    {
        private static RoleService? instance;
        private Repository<Role> roleRepository;

        private RoleService()
        {
            roleRepository = Repository<Role>.GetInstance;
        }

        public static RoleService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoleService();
                }

                return instance;
            }
        }

        public void Add(Role role)
        {
            roleRepository.Add(role);
        }

        public List<Role> GetAll()
        {
            return roleRepository.GetAll();
        }

        public void Update(Role role)
        {
            roleRepository.Update(role);
        }

        public void Delete(int id)
        {
            roleRepository.DeleteById(id);
        }
    }
}
