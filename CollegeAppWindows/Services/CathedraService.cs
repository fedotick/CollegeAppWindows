using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class CathedraService
    {
        private Repository<Cathedra> cathedraRepository = Repository<Cathedra>.GetInstance;

        private static CathedraService? instance;

        private CathedraService() { }

        public static CathedraService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CathedraService();
                }

                return instance;
            }
        }

        public List<Cathedra> GetAll()
        {
            return cathedraRepository.GetAll();
        }
    }
}
