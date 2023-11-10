using Microsoft.EntityFrameworkCore;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly TPIContext _context;
        public AdminService(TPIContext context)
        {
                _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }


        public void DeleteUser(int userId)
        {
            User? userToBeDeleted = _context.Users.FirstOrDefault(u => u.UserId == userId);
            userToBeDeleted.Status = false;
            _context.Update(userToBeDeleted);
            _context.SaveChanges();
        }


    }
}
