using JWTSpa_Autentication.Enumerations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Validate
{
    public class ValidacionArchivoExtension:ValidationAttribute
    {
        private readonly string[] _tiposValidos;

        public ValidacionArchivoExtension(string[] tiposValidos)
        {
            _tiposValidos = tiposValidos;
        }
        public ValidacionArchivoExtension(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                _tiposValidos = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if (!_tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes:{string.Join(",",_tiposValidos)}");    
            }
            return ValidationResult.Success;
        }



    }
}
