using hotel_management_api_identity.Features.Authentication.Models;

namespace hotel_management_api_identity.Features.Authentication.Config
{
    public static class TokenExtensions
    {
        public static TokenResponse ToToken(this Core.Storage.Models.Tokens tokenData)
        {
            return new TokenResponse()
            {
                Email = tokenData.CreatedById,
                Token = tokenData.Token,
                ExpiryDate = tokenData.ExpiryDate
            };
        }

        public static Core.Storage.Models.Tokens ToDbToken(this TokenRequest tokenData)
        {
            return new Core.Storage.Models.Tokens()
            {
                CreatedById = tokenData.Email,
                ModifiedById = tokenData.Email,
                Token = tokenData.Token,
                ExpiryDate = tokenData.ExpiryDate
            };
        }
    }
}