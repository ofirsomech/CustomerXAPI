using GitHubSearchAPI.Dtos;
using GitHubSearchAPI.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GitHubSearchAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;

        public AuthService(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public bool Authenticate(LoginDto loginDto)
        {
            return loginDto.Username == "root" && loginDto.Password == "123456";
        }

        public string GenerateJwtToken(string username)
        {
            return _jwtService.GenerateToken(username);
        }
    }
}
