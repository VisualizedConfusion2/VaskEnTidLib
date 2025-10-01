using VaskEnTidLib.Models;
using VaskEnTidLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaskEnTidLib.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();

        // Implementing the interface methods explicitly
        User? IUserService.GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        IEnumerable<User> IUserService.GetAllUsers()
        {
            return _users;
        }

        User IUserService.CreateUser(User user)
        {
            user.UserId = _users.Count > 0 ? _users.Max(u => u.UserId) + 1 : 1;
            _users.Add(user);
            return user;
        }

        bool IUserService.UpdateUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existing == null) return false;

            existing.ApartmentNumber = user.ApartmentNumber;
            existing.Name = user.Name;
            existing.Phone = user.Phone;
            existing.Email = user.Email;
            existing.Password = user.Password;
            existing.DepartmentID = user.DepartmentID;
            existing.UserTypeID = user.UserTypeID;
            return true;
        }

        bool IUserService.DeleteUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }

        IEnumerable<User> IUserService.GetUsersByDepartmentId(int departmentId)
        {
            return _users.Where(u => u.DepartmentID == departmentId);
        }

        async Task<User> IUserService.CreateUserAsync(User user)
        {
            // Simulate async operation
            return await Task.Run(() =>
            {
                user.UserId = _users.Count > 0 ? _users.Max(u => u.UserId) + 1 : 1;
                _users.Add(user);
                return user;
            });
        }
    }
}