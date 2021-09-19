using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class SalaDeCineCreacionDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Range(-90,90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }
    }
}
