using System.Security.Claims;
using System.Security.Principal;

namespace hotel_management_api_identity.Core.Helpers.Extension
{
    public static class PrincipalUtilities
    {
        public static string GetEmail(this IIdentity identity)
        {
            return GetClaimValue(identity, ClaimTypes.Email);
        }

        private static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
        {
            var claimsList = new List<Claim>(claims);
            return claimsList.Find(c => c.Type == claimType) != null ? claimsList.Find(c => c.Type == claimType).Value : null;
        }
        private static string GetClaimValue(this IIdentity identity, string claimType)
        {
            var claimIdentity = (ClaimsIdentity)identity;
            return claimIdentity.Claims.GetClaimValue(claimType);
        }
    }
}