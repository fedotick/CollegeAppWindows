using CollegeAppWindows.Models;
using CollegeAppWindows.Repositories;
using System.Collections.Generic;

namespace CollegeAppWindows.Services
{
    internal class UserService
    {
        private static UserService? instance;
        private Repository<User> userRepository;

        private UserService()
        {
            userRepository = Repository<User>.GetInstance;
        }

        public static UserService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }

                return instance;
            }
        }

        public void Add(User user)
        {
            userRepository.Add(user);
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public void Update(User user)
        {
            userRepository.Update(user);
        }

        public void Delete(int id)
        {
            userRepository.DeleteById(id);
        }

        public bool LogIn(string username, string password)
        {
            List<User> users = userRepository.GetAll();

            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    LoggedInUser.GetInstance(user);
                    return true;
                }
            }

            return false;
        }
    }
}
