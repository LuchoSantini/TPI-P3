using Microsoft.EntityFrameworkCore;
using System.Drawing;
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

        public int AddColour(string colour)
        {
            //Response validateColor = new Response();
            //validateColor.Result = _context.Colours.Any(u => u.ColourName == colour);
            //if (validateColor.Result == false) 
            //{
                Colour colourToAdd = new Colour
                {
                    ColourName = colour,
                };
                _context.Colours.Add(colourToAdd);
                _context.SaveChanges();
            return colourToAdd.Id;
            //    validateColor.Message = "Color agregado";
            //    return validateColor.Message;
            //}
            //validateColor.Message = "Error al agregar el color";
            //return validateColor.Message;
        }

        //public Response ValidateColour(string colour)
        //{
        //    Response validateColor = new Response();
        //    validateColor.Result = _context.Colours.Any(u => u.ColourName == colour);
        //}

        public int AddSize(string size)
        {
            Size sizeToAdd = new Size
            {
                SizeName = size,
            };
            _context.Sizes.Add(sizeToAdd);
            _context.SaveChanges();
            return sizeToAdd.Id;
        }


    }
}
