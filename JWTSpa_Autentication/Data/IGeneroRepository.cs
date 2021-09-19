using JWTSpa_Autentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public interface IGeneroRepository
    {
        Task<List<Genero>> GetGenerosAsync();
        Task<Genero> GetGeneroById(int id);
        Task<bool> UpdateGenero(Genero genero);
        Task<bool> ValidarGenero(int id);
        Task DeleteGenero(Genero genero);
        void AddGenero(Genero genero);
        Task SaveChangesA();
    }
}
