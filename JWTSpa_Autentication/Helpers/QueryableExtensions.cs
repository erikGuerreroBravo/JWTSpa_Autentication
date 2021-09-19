using JWTSpa_Autentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> querybale, PaginacionDto paginacionDto) {
            return querybale.Skip((paginacionDto.Pagina - 1) * paginacionDto.CantidadRegistrosPorPagina)
                .Take(paginacionDto.CantidadRegistrosPorPagina);
        }
    }
}
