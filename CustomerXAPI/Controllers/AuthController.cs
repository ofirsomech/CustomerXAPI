﻿using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerXAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (_authService.Authenticate(loginDto))
            {
                var token = _authService.GenerateJwtToken(loginDto.Username);
                return Ok(new { token });
            }

            return BadRequest("Invalid username or password");
        }
    }
}
