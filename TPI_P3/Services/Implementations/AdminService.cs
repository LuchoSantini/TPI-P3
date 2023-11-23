using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Globalization;
using TPI_P3.Data;
using TPI_P3.Data.Entities;
using TPI_P3.Data.Models;
using TPI_P3.Services.Interfaces;
using Size = TPI_P3.Data.Entities.Size;

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

        public bool CheckIfColourExists(string colour)
        {
            return _context.Colours.Any(c => c.ColourName == colour);
        }
        public bool CheckIfSizeExists(string size)
        {
            return _context.Sizes.Any(s => s.SizeName == size);
        }

        public int AddColour(string colour)
        {
            bool existingColour = CheckIfColourExists(colour);
            if (!existingColour)
            {
                Colour colourToAdd = new Colour
                {
                    ColourName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(colour.ToLower()),
                };
                _context.Colours.Add(colourToAdd);
                _context.SaveChanges();
                return colourToAdd.Id;
            }
            return 1;
        }

        public int AddSize(string size)
        {
            bool existingSize = CheckIfSizeExists(size);
            if (!existingSize)
            {
                Size sizeToAdd = new Size
                {
                    SizeName = size.ToUpper(),
                };
                _context.Sizes.Add(sizeToAdd);
                _context.SaveChanges();
                return sizeToAdd.Id;
            }

            return 1; 
        }


    }
}
