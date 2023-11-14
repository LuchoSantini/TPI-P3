using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;
using TPI_P3.Services.Interfaces;

namespace TPI_P3.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly TPIContext _context;
        public UserService(TPIContext context)
        {
            _context = context;
        }


        public UserResponse ValidateUser(string userName, string password)
        {
            UserResponse response = new UserResponse();
            User? userForLogin = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if (userForLogin != null)
            {
                if (userForLogin.Password == password)
                {
                    response.Result = true;
                    response.Message = "Loging Succesfull";
                }
                else
                {
                    response.Result = false;
                    response.Message = "Wrong Password";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Wrong Username";
            }
            return response;
        }

        public User CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User? GetUserByUsername(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        
    }
}
