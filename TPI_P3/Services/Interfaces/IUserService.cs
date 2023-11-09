using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;

namespace TPI_P3.Services.Interfaces
{
    public interface IUserService
    {
        public User? GetUserByUsername(string userName);
        public UserResponse ValidateUser(string userName, string password);
        public int CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int userId);
    }
}
