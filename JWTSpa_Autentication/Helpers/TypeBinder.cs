using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        private readonly ILogger<TypeBinder<T>> _logger;
        public TypeBinder(ILogger<TypeBinder<T>> logger)
        {
            _logger = logger;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);
            if (proveedorDeValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(proveedorDeValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                bindingContext.ModelState.TryAddModelError(nombrePropiedad,"Valor invalido para List<int>");
            }
            return Task.CompletedTask;
        }
    }
}
