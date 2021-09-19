using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class ActorPeliculasCreacionDto
    {
        public int ActorId { get; set; }
        public string Mensaje { get; set; }
        /*este campo debe ser heredado*/
        public string Personaje { get; set; }
    }
}
