using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class PeliculaDetalleDto:PeliculasDto
    {
        public List<GeneroDto> Generos { get; set; }
        public List<ActorPeliculaDetalleDto> Actores { get; set; }
    }

}
