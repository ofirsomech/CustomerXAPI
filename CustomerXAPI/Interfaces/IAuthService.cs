using GitHubSearchAPI.Dtos;

namespace GitHubSearchAPI.Interfaces
{
    public interface IAuthService
    {
        bool Authenticate(LoginDto loginDto);

        string GenerateJwtToken(string username);
    }
}
