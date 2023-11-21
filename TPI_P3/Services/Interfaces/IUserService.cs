using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;

namespace TPI_P3.Services.Interfaces
{
    public interface IUserService
    {

        public Response ValidateUser(string userName, string password);
        public User CreateUser(User user);
        public User? GetUserByUsername(string userName);
        public void UpdateUser(User user);
        public void DeleteUser(int userId);

    }
}
