using VaskEnTidLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace VaskEnTidLib.Interfaces
{
    public interface IUserService
    {
        User? GetUserById(int userId);
        User? CreateUser(int userId, int targetUserId);
        bool UpdateUser(int userId, int targetUserId);
        bool DeleteUser(int userId, int targetUserId);
        User? GetUsersByDepartmentId(int departmentId, int userId);
    }
}