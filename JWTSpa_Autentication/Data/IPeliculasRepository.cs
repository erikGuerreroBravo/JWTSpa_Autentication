using JWTSpa_Autentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public interface IPeliculasRepository
    {
        Task<List<Pelicula>> Get();
        Task<Pelicula> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> AddPeliculaAsync(Pelicula pelicula);
        void AddPelicula(Pelicula pelicula);
        Task Save();
        Task<bool> ValidarPeliculas(int id);
        Task<List<Pelicula>> FiltroPelicula(int top, DateTime hoy);
        Task<List<Pelicula>> FiltroEnCines(int top);
        IQueryable FiltroQueryble();

    }
}
