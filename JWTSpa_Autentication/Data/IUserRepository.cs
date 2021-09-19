using JWTSpa_Autentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public interface IUserRepository
    {
        /// <summary>
        /// Este metodo se encarga insertar un usuario
        /// </summary>
        /// <param name="user">entidad user</param>
        /// <returns>regresa el usuario creado</returns>
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
    }
}
