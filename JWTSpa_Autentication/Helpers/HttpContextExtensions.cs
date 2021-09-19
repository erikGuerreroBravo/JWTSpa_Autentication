using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext,
            IQueryable<T> queryble, int cantidadRegistrosPorPagina)
        {
            double cantidad = await queryble.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad/cantidadRegistrosPorPagina);
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }
    }
}
