using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class SalaDeCineDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
