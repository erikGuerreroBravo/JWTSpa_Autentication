using JWTSpa_Autentication.Data;
using JWTSpa_Autentication.Dtos;
using JWTSpa_Autentication.Helpers;
using JWTSpa_Autentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger; 
        public AuthController(IUserRepository repository, JwtService jwtService, ILogger<AuthController> logger)
        {
            _repository = repository;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost(template: "register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword( registerDto.Password)
            };
            //_repository.Create(user);
            //return Ok("success");
            return Created(uri: "success", value: _repository.Create(user));
        }

        [HttpPost(template: "login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _repository.GetByEmail(loginDto.Email);
            if (user == null)
            {
                return BadRequest(error:new { message="Credenciales Invalidas"});
            }
            if (!BCrypt.Net.BCrypt.Verify(text: loginDto.Password, hash: user.Password))
            {
                return BadRequest(error:new { message="Credenciales Invalidas"});
            }
            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {

                HttpOnly = true
            });

            return Ok(new { 
              message="success"
            } );
        }

        [HttpGet("user")]
        public IActionResult Usuario()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repository.GetById(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return Unauthorized();
                
            }
            
        }

    }
}
