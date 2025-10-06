using VaskEnTidLib.Models;
using VaskEnTidLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaskEnTidLib.Services
{
    public class UserService : IUserService
    {
        public User? CreateUser(int userId, int targetUserId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId, int targetUserId)
        {
            throw new NotImplementedException();
        }

        public User? GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public User? GetUsersByDepartmentId(int departmentId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(int userId, int targetUserId)
        {
            throw new NotImplementedException();
        }
    }
}