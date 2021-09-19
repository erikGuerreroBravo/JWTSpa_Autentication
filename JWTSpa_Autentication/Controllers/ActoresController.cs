using AutoMapper;
using JWTSpa_Autentication.Data;
using JWTSpa_Autentication.Dtos;
using JWTSpa_Autentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JWTSpa_Autentication.Helpers;
using Microsoft.EntityFrameworkCore;

namespace JWTSpa_Autentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : CustomBaseController
    {
        private readonly IActorRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserContext _context;
        public ActoresController(IActorRepository repository, UserContext context, IMapper mapper) : base(context, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet(Name ="listar")]
        public async Task<ActionResult<List<ActorDto>>> Listar([FromQuery]PaginacionDto paginacionDto)
        {
            var queryable = _repository.GetQuerybleActor();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDto.CantidadRegistrosPorPagina);
            var actores = await queryable.Paginar(paginacionDto).ToListAsync();
            return _mapper.Map<List<ActorDto>>(actores);
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> Get()
        {
            var actores = await _repository.GetActoresAsync();
            return _mapper.Map<List<ActorDto>>(actores);
        }
        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            var actor = await _repository.GetActorById(id);
            if (actor == null) { return NotFound(); }
            return _mapper.Map<ActorDto>(actor);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var modelActor= _repository.GetActorById(id);
            if (modelActor == null)
            {
                return NotFound();
            }
            var actorDto = _mapper.Map<ActorPatchDto>(modelActor);
            patchDocument.ApplyTo(actorDto, ModelState);
            var esValido = TryValidateModel(actorDto);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }
            var a =_mapper.Map(actorDto,modelActor);
            await _repository.SaveChangesA();
            return NoContent();

            //return await Patch<Actor, ActorPatchDto>(id,patchDocument);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDto actorCreacionDto)
        {
            var actor = _mapper.Map<Actor>(actorCreacionDto);
            _repository.AddActor(actor);
            await _repository.SaveChangesA();
            var dtoActor = _mapper.Map<ActorDto>(actor);
            return new CreatedAtRouteResult("obtenerActor",new { id=actor.Id}, dtoActor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActorCreacionDto actorCreacionDto)
        {
            var actor = _mapper.Map<Actor>(actorCreacionDto);
            actor.Id = id;
            await _repository.UpdateActor(actor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _repository.ValidarActor(id);
            if (!existe) { return NotFound(); }
            await _repository.DeleteActor(new Actor { Id = id });
            return NoContent();

        }
    }
}
