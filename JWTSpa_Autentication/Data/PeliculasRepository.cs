using JWTSpa_Autentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public class PeliculasRepository : IPeliculasRepository
    {
        private readonly UserContext _context;
        public PeliculasRepository(UserContext context)
        {
            _context = context;
        }

        public void AddPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
        }

        public async Task<bool> AddPeliculaAsync(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pelicula>> Get()
        {
            return _context.Peliculas.ToListAsync();
        }

        public async Task<List<Pelicula>> FiltroPelicula(int top, DateTime hoy)
        {
           return  await _context.Peliculas.Where(x => x.FechaEstreno > hoy)
                .OrderBy(x => x.FechaEstreno)
                .Take(top).ToListAsync();
        }
        public async Task<List<Pelicula>> FiltroEnCines(int top) {
            return await _context.Peliculas.
                Where(x => x.EnCines)
                .Take(top)
                .ToListAsync();
        }

        public async Task<Pelicula> GetById(int id)
        {
            return await _context.Peliculas
                .Include(x=>x.PeliculasActores).ThenInclude(x=>x.Actor)
                .Include(x=> x.PeliculasGeneros).ThenInclude(x=>x.Genero)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<bool> ValidarPeliculas(int id)
        {
            return await _context.Peliculas.AnyAsync(p => p.Id == id);
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable FiltroQueryble()
        {
            return _context.Peliculas.AsQueryable<Pelicula>();
        }


    }
}
