using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Models
{
    public class Status: IId
    {
        public int Id { get; set; }
        public string StrValor { get; set; }
        public string StrDescripcion { get; set; }
    }
}
