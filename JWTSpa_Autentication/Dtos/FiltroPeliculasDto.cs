using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class FiltroPeliculasDto
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;
        public PaginacionDto Paginacion {
            get { return new PaginacionDto() { Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina }; }
        }
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }

        public string CampoOrdenar { get; set; }
        public bool OrdenAscendente { get; set; } = true;


    }
}
