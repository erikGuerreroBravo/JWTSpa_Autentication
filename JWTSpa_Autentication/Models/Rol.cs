using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Models
{
    public class Rol: IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength=3,ErrorMessage="El nombre del rol debe ser valido")]
        public string StrValor { get; set; }
        [StringLength(256)]
        public string StrDescripcion { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public List<User> Usuarios { get; set; }
    }
}
