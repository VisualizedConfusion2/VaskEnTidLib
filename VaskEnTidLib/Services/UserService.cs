using VaskEnTidLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaskEnTidLib.Repositories;

namespace VaskEnTidLib.Services
{
    public class UserService
    {
        private readonly UserRepo _repo;
        public UserService(UserRepo repo) => _repo = repo;
        public bool TryAuthenticate(string email, string password, out User? user)
        {
            user = _repo.GetUserByEmail(email);
            if (user == null) return false;
            return user.Password == password; // plain-text check
        }

        public User? RegisterUserByCreationCode(string creationCode, string phone, string email, string password)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(creationCode))
            {
                return null;
            }
            return _repo.RegisterUserByCreationCode(creationCode, phone, email, password);
        }

    }
}