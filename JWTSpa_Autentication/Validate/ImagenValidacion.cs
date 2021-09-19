using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Validate
{
    public class ImagenValidacion:ValidationAttribute
    {
        private readonly int _sizeMaxMegaBytes;
        public ImagenValidacion(int sizeMaxMegaBytes)
        {
            _sizeMaxMegaBytes = sizeMaxMegaBytes;
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
            if (formFile.Length > _sizeMaxMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El tamaño del archivo no debe ser mayo a {_sizeMaxMegaBytes}MB");
            }
            return ValidationResult.Success;
        }


    }
}
