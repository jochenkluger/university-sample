using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace UniversitySample.Shared
{
    public class KeycloakRolesClaimsTransformer: IClaimsTransformation
    {
        private readonly string _roleClaimType;
        private readonly string _audience;

        public KeycloakRolesClaimsTransformer(string roleClaimType, string audience)
        {
            _roleClaimType = roleClaimType;
            _audience = audience;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            return TransformRolesAsync(principal, _audience, _roleClaimType);
        }

        public static Task<ClaimsPrincipal> TransformRolesAsync(ClaimsPrincipal principal, string audience,
            string roleClaimType)
        {
            var result = principal.Clone();
            if (result.Identity is not ClaimsIdentity identity)
            {
                return Task.FromResult(result);
            }

            var resourceAccessValue = principal.FindFirst("resource_access")?.Value;
            if (string.IsNullOrWhiteSpace(resourceAccessValue))
            {
                return Task.FromResult(result);
            }

            using var resourceAccess = JsonDocument.Parse(resourceAccessValue);
            var clientRoles = resourceAccess
                .RootElement
                .GetProperty(audience)
                .GetProperty("roles");

            foreach (var role in clientRoles.EnumerateArray())
            {
                var value = role.GetString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    identity.AddClaim(new Claim(roleClaimType, value));
                }
            }

            return Task.FromResult(result);
        }
    }
}