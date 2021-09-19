using AutoMapper;
using JWTSpa_Autentication.Data;
using JWTSpa_Autentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public CustomBaseController(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected async Task<List<TDto>> Get<TEntidad,TDto>() where TEntidad :class
        {
            var entidades = await _context.Set<TEntidad>().AsNoTracking().ToListAsync();
            var dtos = _mapper.Map<List<TDto>>(entidades);
            return dtos;
        }

        protected async Task<ActionResult<TDto>> Get<TEntidad, TDto>(int id) where TEntidad : class, IId
        {
            var entidad = await _context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(p=> p.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            return  _mapper.Map<TDto>(entidad);
            
        }

        protected async Task<ActionResult> Post<TCreacion, TEntidad, TLectura>(TCreacion creacionDto, string nombreRuta) where TEntidad : class, IId
        {
            var entidad = _mapper.Map<TEntidad>(creacionDto);
            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var dtoLectura = _mapper.Map<TLectura>(entidad);
            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, dtoLectura);
        }


        protected async Task<ActionResult> Put<TCreacion, TEntidad>(int id, TCreacion creacionDTo) where TEntidad : class, IId
        {
            var entidad = _mapper.Map<TEntidad>(creacionDTo);
            entidad.Id = id;
            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new()
        {
            var existe = await _context.Set<TEntidad>().AnyAsync(x=> x.Id == id);
            if (!existe) {
                return NotFound();
            }
            _context.Remove(new TEntidad { Id = id});
            await _context.SaveChangesAsync();
            return NoContent();
        }

        protected async Task<ActionResult> Patch<TEntidad, TDto>(int id, JsonPatchDocument<TDto> patchDocument) 
            where TDto : class where TEntidad : class, IId
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await _context.Set<TEntidad>().FirstOrDefaultAsync(x=>x.Id == id);
            if (entidadDB == null)
            {
                return NotFound();
            }
            var entidadDto = _mapper.Map<TDto>(entidadDB);
            patchDocument.ApplyTo(entidadDto,ModelState);
            var esValido = TryValidateModel(entidadDto);
            if (!esValido)
            {
                return BadRequest(ModelState);

            }
            _mapper.Map(entidadDto,entidadDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
