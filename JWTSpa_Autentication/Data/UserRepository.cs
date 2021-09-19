using JWTSpa_Autentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id =_context.SaveChanges();
            return user;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(p=> p.Email == email);
        }
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id ==id );
        }
    }
}
