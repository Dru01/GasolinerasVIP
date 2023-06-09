using System.Security.Claims;

namespace GasolinerasVIP.API.Models
{
    public interface IJWTManagerRepository
    {
        Token GenerateToken(string userName);
        Token GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
