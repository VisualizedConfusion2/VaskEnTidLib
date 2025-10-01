using VaskEnTidLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace VaskEnTidLib.Interfaces
{
    public interface IUserService
    {
        User? GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        User CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);

        // Show users based on department id
        IEnumerable<User> GetUsersByDepartmentId(int departmentId);

        // Create a new user (async version)
        Task<User> CreateUserAsync(User user);
    }
}