using JWTSpa_Autentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public class RolRepository: IRolRepository
    {
        private readonly UserContext _context;
        public RolRepository(UserContext context)
        {
            _context = context;
        }

        
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rol>> Get()
        {
            return await _context.Roles.Include(p=>p.Status).ToListAsync();
        }

        public async Task<Rol> GetById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(p=>p.Id == id);
        }


        public void AddRol(Rol rol)
        {
             rol.StatusId = 1;
            _context.Roles.Add(rol);
            _context.SaveChanges();
        }
        public async Task<bool> AddRolAsync(Rol rol)
        {
            rol.StatusId = 1;
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
