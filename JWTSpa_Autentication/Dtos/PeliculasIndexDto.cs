using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class PeliculasIndexDto
    {
        public List<PeliculasDto> FuturosEstrenos { get; set; }
        public List<PeliculasDto> EnCines { get; set; }
    }
}
