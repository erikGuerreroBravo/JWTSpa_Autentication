using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class RolCreacionDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre del rol debe ser valido")]
        public string StrValor { get; set; }
        [StringLength(256)]
        public string StrDescripcion { get; set; }
    }
}
