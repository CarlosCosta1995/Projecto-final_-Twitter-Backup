using TwitterClone_Carlos2036114_Ines2036314.Models;

namespace TwitterClone_Carlos2036114_Ines2036314.Services
{
    public interface IJWTService
    {
        string GenerateToken(string key, string issuer, string audience, User user);
        bool IsTokenValid(string key, string issuer, string audience, string token);

        public string GetJWTTokenClaim(string token);

    }
}
