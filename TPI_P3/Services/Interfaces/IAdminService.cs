using TPI_P3.Data.Entities;

namespace TPI_P3.Services.Interfaces
{
    public interface IAdminService
    {
        public List<User> GetUsers();
        public void DeleteUser(int userId);


    }
}
