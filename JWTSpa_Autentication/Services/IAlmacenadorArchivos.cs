using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Services
{
    public interface IAlmacenadorArchivos
    {
        Task BorrarArchivo(string ruta, string contenedor);
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType);
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor,string contentType);
    }
}
