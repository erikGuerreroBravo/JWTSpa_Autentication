using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
