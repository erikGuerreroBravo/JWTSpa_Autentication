using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Models
{
    public class PeliculasGeneros 
    {

        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }

        
    }
}
