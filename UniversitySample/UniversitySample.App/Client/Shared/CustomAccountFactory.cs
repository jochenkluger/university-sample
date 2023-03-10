using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.VisualBasic;
using System.Security.Claims;
using UniversitySample.App.Shared;

namespace UniversitySample.App.Client.Shared
{
    public class CustomAccountFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomAccountFactory(IAccessTokenProviderAccessor accessor, HttpClient httpClient)
            : base(accessor)
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity != null && initialUser.Identity.IsAuthenticated)
            {
                return await KeycloakRolesClaimsHelper.TransformRolesAsync(initialUser, "dhbw-uni-sample-wasm",
                    "roles");
            }

            return initialUser;
        }
    }
}
