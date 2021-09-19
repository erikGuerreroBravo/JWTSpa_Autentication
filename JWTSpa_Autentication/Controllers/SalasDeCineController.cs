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
    [Route("api/salasdecine")]
    [ApiController]
    public class SalasDeCineController : CustomBaseController
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        public SalasDeCineController(UserContext context, IMapper mapper) : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDto>>> Get()
        {
            return await Get<SalaDeCine, SalaDeCineDto>();  
        }
        [HttpGet("{id:int}", Name = "obtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDto>> Get(int id)
        {
            return await Get<SalaDeCine, SalaDeCineDto>(id);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaDeCineCreacionDto salaDeCineCreacionDto)
        {
            return await Post<SalaDeCineCreacionDto, SalaDeCine, SalaDeCineDto>(salaDeCineCreacionDto,"obtenerSalaDeCine");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SalaDeCineCreacionDto salaDeCineCreacionDto)
        {
            return await Put<SalaDeCineCreacionDto, SalaDeCine>(id, salaDeCineCreacionDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SalaDeCine>(id);
        }

    }
}
