using JWTSpa_Autentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public interface IRolRepository
    {
        Task<List<Rol>> Get();
        Task<Rol> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> AddRolAsync(Rol rol);
        void AddRol(Rol rol);
        Task Save();
    }
}
