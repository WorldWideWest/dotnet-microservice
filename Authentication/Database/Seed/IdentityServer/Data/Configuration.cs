using IdentityServer4.Models;

namespace Database.Seed.IdentityServer.Data
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "Authentication",
                    UserClaims = new List<string> { "anonymous" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("AuthenticationAPI.read"),
                new ApiScope("AuthenticationAPI.write")
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
                    ApiSecrets = new List<Secret> { new Secret("sa21dfg324".Sha512())},
                    UserClaims = new List<string> { "Authentication" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "Authentication.web",
                    ClientName = "Web Authentication Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511fde5yj8".Sha512()) },
                    AllowedScopes = { "AuthenticationAPI.read", "AuthenticationAPI.write" }
                }
            };
    }
}

