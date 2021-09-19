using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class PeliculasDto
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [StringLength(300)]
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
    }
}
