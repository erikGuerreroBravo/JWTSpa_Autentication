using JWTSpa_Autentication.Enumerations;
using JWTSpa_Autentication.Validate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class ActorCreacionDto: ActorPatchDto
    {
        
        [ImagenValidacion(sizeMaxMegaBytes:4)]
        [ValidacionArchivoExtension(grupoTipoArchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
