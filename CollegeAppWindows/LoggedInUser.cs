using CollegeAppWindows.Models;
using System;

namespace CollegeAppWindows
{
    internal class LoggedInUser
    {
        private static User user;
        private static LoggedInUser? instance;

        private LoggedInUser(User user)
        {
            LoggedInUser.user = user;
        }

        public static LoggedInUser GetInstance(User user)
        {
            if (instance == null)
            {
                instance = new LoggedInUser(user);
            }

            return instance;
        }

        public static LoggedInUser GetInstance()
        {
            if (instance == null)
            {
                throw new InvalidOperationException("Instance is not initialized. Call GetInstance(User user) first.");
            }

            return instance;
        }

        public User GetUser()
        {
            return user;
        }
    }
}
