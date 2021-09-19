using AutoMapper;
using JWTSpa_Autentication.Data;
using JWTSpa_Autentication.Dtos;
using JWTSpa_Autentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : CustomBaseController
    {
        private readonly UserContext _context;
        private readonly IGeneroRepository _repository;
        private readonly IMapper _mapper;
        public GenerosController(UserContext context, IGeneroRepository repository, IMapper mapper):base(context,mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<GeneroDto>>> Get()
        //{
        //    var generos = await _repository.GetGenerosAsync();
        //    var dtoGeneros = _mapper.Map<List<GeneroDto>>(generos);
        //    return dtoGeneros;
        //}

        [HttpGet]
        public async Task<ActionResult<List<GeneroDto>>> Get()
        {
            //utilizamos una consulta geenrica del controlador heredado.
            return await Get<Genero, GeneroDto>();
        }




        [HttpGet("{id:int}", Name ="obtenerGenero")]
        public async Task<ActionResult<GeneroDto>> GetById(int id)
        {
            return await Get<Genero, GeneroDto>(id);
            //var genero = await _repository.GetGeneroById(id);
            //if (genero == null)
            //{
            //    return NotFound();
            //}
            //var dtoGenero = _mapper.Map<GeneroDto>(genero);
            //return dtoGenero;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDto creacionDto)
        {
            //var genero = _mapper.Map<Genero>(creacionDto);
            //_repository.AddGenero(genero);
            //await _repository.SaveChangesA();
            //var generoDto = _mapper.Map<GeneroDto>(genero);
            //return new CreatedAtRouteResult("obtenerGenero", new { id = generoDto.Id }, generoDto);
            return await Post<GeneroCreacionDto, Genero, GeneroDto>(creacionDto,"obtenerGenero");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDto generoCreacionDto)
        {
            //var genero = _mapper.Map<Genero>(generoCreacionDto);
            //genero.Id = id;
            //await _repository.UpdateGenero(genero);
            //return NoContent();
            return await Put<GeneroCreacionDto, Genero>(id,generoCreacionDto);
        }
        [HttpDelete("{id}", Name = "eliminar")]
        public async Task<ActionResult> Delete(int id)
        {
            //var existe = await _repository.ValidarGenero(id);
            //if (!existe) { return NotFound(); }
            //await _repository.DeleteGenero(new Genero { Id = id });
            //return NoContent();
            return await Delete<Genero>(id);
        }

                



    }
}
