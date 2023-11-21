using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;

namespace TPI_P3.Services.Interfaces
{
    public interface IAdminService
    {
        public List<User> GetUsers();
        public void DeleteUser(int userId);
        public int AddColour(string colour);
        public int AddSize(string size);

    }
}
