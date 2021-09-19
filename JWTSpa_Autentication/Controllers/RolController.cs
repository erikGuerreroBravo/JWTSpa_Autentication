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
    public class RolController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IRolRepository _repository;
        private readonly IMapper _mapper;
        public RolController(UserContext context, IRolRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<RolDto>>> Get()
        {
            var roles = await _repository.Get();
            var dtoRoles = _mapper.Map<List<RolDto>>(roles);
            return dtoRoles;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RolCreacionDto rolCreacionDto)
        {
            var rol = _mapper.Map<Rol>(rolCreacionDto);
            await _repository.AddRolAsync(rol);
            var rolDto = _mapper.Map<RolDto>(rol);
            return new CreatedAtRouteResult("obtenerRol", new { id = rolDto.Id}, rolDto);
        }

        [HttpGet("{id:int}", Name = "obtenerRol")]
        public async Task<ActionResult<RolDto>> GetById(int id)
        {
            var rol = await _repository.GetById(id);
            if (rol == null)
            {
                return NotFound();
            }
            var dtoRol = _mapper.Map<RolDto>(rol);
            return dtoRol;
        }
    }
}
