using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.MiddlewareExtensions;
using hotel_management_api_identity.Core.Storage.Models;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Authentication.Config;
using hotel_management_api_identity.Features.Authentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hotel_management_api_identity.Features.Authentication.Services
{
    public interface ITokenService : IAutoDependencyCore
    {
        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string CreateToken(string email);

        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        Task<bool> ValidateToken(TokenRequest tokenRequest);  
    }
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IDapperQuery<Employee> _employeeQuery;
        private readonly IDapperCommand<Tokens> _tokenCommand;
        private readonly IOptions<JwtToken> _appSettings;
        public TokenService(ILogger<TokenService> logger, IDapperQuery<Employee> employeeQuery, IOptions<JwtToken> appSettings, IDapperCommand<Tokens> tokenCommand)
        {
            _logger = logger;
            _employeeQuery = employeeQuery;
            _tokenCommand = tokenCommand;
            _appSettings = appSettings;
        }
        
        public string CreateToken(string email)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "email", email } };
                var result = _employeeQuery.GetByDefaultAsync(query).Result;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.ToString()),
                    new Claim(ClaimTypes.Email, result.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, result.UserType.Description())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = new DateTimeOffset(DateTime.Now).DateTime,
                    Issuer = _appSettings.Value.Issuer,
                    Audience = _appSettings.Value.Audience,
                    Expires = DateTime.Now.AddHours(Convert.ToInt32(_appSettings.Value.Expiry)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.Secret)), SecurityAlgorithms.EcdsaSha512Signature)
                 };
                var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
                SaveToken(new TokenRequest { Email = email, ExpiryDate = (DateTimeOffset)tokenDescriptor.Expires, Token = token}).Wait();
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return string.Empty;
            }
        }

        public async Task<bool> ValidateToken(TokenRequest tokenRequest)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "Token", tokenRequest.Token }, { "CreatedById", tokenRequest.Email } };
                var result = await _employeeQuery.ValidateTokenAsync(query);
                if (result is not null) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private async Task SaveToken(TokenRequest tokenRequest)
        {
            try
            {
                await _tokenCommand.AddAsync(tokenRequest.ToDbToken());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}