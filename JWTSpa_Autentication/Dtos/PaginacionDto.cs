using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class PaginacionDto
    {
        public int Pagina { get; set; }
        private int cantidadRegistrosPorPagina = 10;
        private readonly int cantidadMaximaRegistrosPorPagina = 50;

        public int CantidadRegistrosPorPagina {
            get => cantidadMaximaRegistrosPorPagina;
            set {
                cantidadRegistrosPorPagina = (value > cantidadMaximaRegistrosPorPagina) ? cantidadMaximaRegistrosPorPagina : value;
            }
        }


    }
}
