using JWTSpa_Autentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly UserContext _context;
        public GeneroRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await _context.Generos.ToListAsync();
        }

        public async Task<Genero> GetGeneroById(int id)
        {
            return await _context.Generos.FirstOrDefaultAsync(p=>p.Id == id);
        }
        public async Task<bool> ValidarGenero(int id)
        {
            return await _context.Generos.AnyAsync(p => p.Id == id);
             
        }
        public async Task<bool> UpdateGenero(Genero genero)
        {
            _context.Entry(genero).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteGenero(Genero genero)
        {
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
        }
        public void AddGenero(Genero genero)
        {
            _context.Generos.Add(genero);

        }
        public async Task SaveChangesA()
        {
            await _context.SaveChangesAsync();
        }

    }
}
