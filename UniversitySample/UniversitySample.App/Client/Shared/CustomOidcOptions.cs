using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Text.Json.Serialization;

namespace UniversitySample.App.Client.Shared
{
    public class CustomOidcOptions : OidcProviderOptions
    {
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
    }
}
