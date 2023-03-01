using IdentityServer4.Models;

namespace Api.Extensions.Identity
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "anonymous",
                    UserClaims = new List<string> { "anonymous" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("AuthenticationApi.read"),
                new ApiScope("AuthenticationApi.write")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = "web",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("htc3Tsjbye".Sha256())
                    },
                    AllowedScopes =
                    {
                        "AuthenticationApi.read", 
                        "AuthenticationApi.write"
                    }
                }
            };

    }
}
