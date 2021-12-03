using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace hotel_management_api_identity.Core.Helpers.Extension
{
    public static class PrincipalUtilities
    {
        public static string GetEmail(string jwtToken, string audience, string issuer, string secret)
        {
            return ValidateToken(jwtToken, audience, issuer, secret)?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static IEnumerable<Claim> GetRoles(string jwtToken, string audience, string issuer, string secret)
        {
            return ValidateToken(jwtToken, audience, issuer, secret)?.FindAll(ClaimTypes.Role)?.ToList();
        }

        private static ClaimsPrincipal ValidateToken(string jwtToken, string audience, string issuer, string secret)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = audience.ToLower();
            validationParameters.ValidIssuer = issuer.ToLower();
            validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out _);

            return principal;
        }
    }
}