using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class GeneroCreacionDto
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
