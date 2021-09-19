using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Models
{
    public class Pelicula : IId
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [StringLength(300)]
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }

        public List<PeliculasActores> PeliculasActores { get; set; }
        public List<PeliculasGeneros> PeliculasGeneros { get; set; }
        public List<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; }
    }
}
