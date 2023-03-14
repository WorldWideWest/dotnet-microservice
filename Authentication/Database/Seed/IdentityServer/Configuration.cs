using Duende.IdentityServer.Models;

namespace Database.Seed.IdentityServer
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
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("AuthenticationAPI.read"),
                new ApiScope("AuthenticationAPI.write"),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = "web",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "AuthenticationAPI.read",
                        "AuthenticationAPI.write"
                    }
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("AuthenticationAPI")
                {
                    Scopes = new List<string>
                    {
                        "AuthenticationAPI.read",
                        "AuthenticationAPI.write"
                    },
                    ApiSecrets = new List<Secret> { new Secret("ee53d355ae".Sha512()) },
                    UserClaims = new List<string> { "role" }
                }
            };
    }
}