using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using UniSample.Common.Security;

namespace UniSample.App.Client.Shared
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
            return (initialUser.Identity != null && initialUser.Identity.IsAuthenticated)
                ? await KeycloakClaimsHelper.TransformRolesAsync(initialUser, Constants.Audience)
                : initialUser;
        }
    }
}
