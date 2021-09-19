using AutoMapper;
using JWTSpa_Autentication.Data;
using JWTSpa_Autentication.Dtos;
using JWTSpa_Autentication.Models;
using JWTSpa_Autentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JWTSpa_Autentication.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;

namespace JWTSpa_Autentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPeliculasRepository _repository;
        private readonly IAlmacenadorArchivos _archivos;
        private readonly string contenedor = "peliculas";
        private readonly UserContext _context;
        private readonly ILogger<PeliculasController> _logger;
        public PeliculasController(IMapper mapper, IPeliculasRepository repository, 
            IAlmacenadorArchivos archivos, UserContext context, ILogger<PeliculasController> logger)
        {
            _mapper = mapper;
            _archivos = archivos;
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PeliculasIndexDto>> Get()
        {
            var top = 5;
            var hoy = DateTime.Today;
            
            var proximosEstrenos= await _repository.FiltroPelicula(top,hoy);
            var enCines = await _repository.FiltroEnCines(top);

            var resultado = new PeliculasIndexDto();
            resultado.FuturosEstrenos = _mapper.Map<List<PeliculasDto>>(proximosEstrenos);
            resultado.EnCines = _mapper.Map <List<PeliculasDto>> (enCines);
            return resultado;

            //var peliculas = await _repository.Get();
            //return _mapper.Map<List<PeliculasDto>>(peliculas);
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculasDto>>> Filtrar([FromQuery] FiltroPeliculasDto filtroPeliculasDto)
        {
            var peliculasQueryable = _context.Peliculas.AsQueryable();
           
            if (!string.IsNullOrEmpty(filtroPeliculasDto.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Titulo.Contains(filtroPeliculasDto.Titulo));
            }
            if (filtroPeliculasDto.EnCines)
            {
                peliculasQueryable = peliculasQueryable.Where(x=>x.EnCines);
            }
            if (filtroPeliculasDto.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(x=> x.FechaEstreno > hoy);
            }
            if (filtroPeliculasDto.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable
                    .Where(x => x.PeliculasGeneros.Select(y => y.GeneroId)
                    .Contains(filtroPeliculasDto.GeneroId)
                );
            }
            if (!string.IsNullOrEmpty(filtroPeliculasDto.CampoOrdenar))
            {
                var tipoOrden = filtroPeliculasDto.OrdenAscendente ? "ascending" : "descending";
                try
                {
                    peliculasQueryable = peliculasQueryable.OrderBy($"{filtroPeliculasDto.CampoOrdenar} {tipoOrden}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message,ex);
                    
                }
                
            }




            await HttpContext.InsertarParametrosPaginacion(peliculasQueryable, filtroPeliculasDto.CantidadRegistrosPorPagina);
            var peliculas = await peliculasQueryable.Paginar(filtroPeliculasDto.Paginacion).ToListAsync();
            return _mapper.Map<List<PeliculasDto>>(peliculas);
        }

         
        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDetalleDto>> Get(int id)
        {
            var pelicula = await _repository.GetById(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            pelicula.PeliculasActores = pelicula.PeliculasActores.OrderBy(x=>x.Orden).ToList();
            return _mapper.Map<PeliculaDetalleDto>(pelicula);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]PeliculasCreacionDto peliculasCreacionDto)
        {
            var pelicula = _mapper.Map<Pelicula>(peliculasCreacionDto);
            if (peliculasCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculasCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculasCreacionDto.Poster.FileName);
                    pelicula.Poster = await _archivos.GuardarArchivo(contenido, extension, contenedor, peliculasCreacionDto.Poster.ContentType);
                }
            }

            AsignarOrdenActores(pelicula);
            _repository.AddPelicula(pelicula);
            await _repository.Save();
            var peliculaDto = _mapper.Map<PeliculasDto>(pelicula);
            return new CreatedAtRouteResult("obtenerPelicula", new { id = pelicula.Id }, peliculaDto.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] PeliculasCreacionDto peliculasCreacionDto)
        {
            var pelicula = _repository.GetById(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            
            var peliculaDB = _mapper.Map(peliculasCreacionDto, pelicula);

            if (peliculasCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculasCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculasCreacionDto.Poster.FileName);
                    pelicula.Result.Poster = await _archivos.EditarArchivo(contenido, extension, contenedor, peliculaDB.Result.Poster,
                        peliculasCreacionDto.Poster.ContentType);
                }
            }
            AsignarOrdenActores(peliculaDB.Result);
            await _repository.Save();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _repository.ValidarPeliculas(id);
            if (!existe)
            {
                return NotFound();
            }
            await _repository.Delete( id );
            await _repository.Save();
            return NoContent();
        }

        private void AsignarOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActores != null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i;
                }
            }
        }


    }
}
