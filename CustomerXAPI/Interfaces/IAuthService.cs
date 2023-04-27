using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface IAuthService
    {
        bool Authenticate(LoginDto loginDto);

        string GenerateJwtToken(string username);
    }
}
