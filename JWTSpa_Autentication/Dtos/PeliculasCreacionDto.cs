using JWTSpa_Autentication.Helpers;
using JWTSpa_Autentication.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Dtos
{
    public class PeliculasCreacionDto
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [StringLength(300)]
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        [ImagenValidacion(sizeMaxMegaBytes:4)]
        [ValidacionArchivoExtension(Enumerations.GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType =typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }
        [ModelBinder(BinderType =typeof(TypeBinder<List<ActorPeliculasCreacionDto>>))]
        public List<ActorPeliculasCreacionDto> Actores { get; set; }

    }
}
