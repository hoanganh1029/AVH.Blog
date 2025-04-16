using Duende.IdentityServer.Models;

namespace AVBlog.WebApp.Configurations
{
    public static class IdentityConfig
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            [   new ApiScope("API.Read", "Read blog"), new ApiScope("API.Write", "Update blog")];

        public static IEnumerable<Client> Clients =>
            [
                new Client
                {
                    ClientId = "WebAPIClient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = ["https://localhost:7001/swagger/oauth2-redirect.html"],

                    AllowedCorsOrigins = { "https://localhost:7001" },
                    ClientSecrets = { new Secret("Abc@123".Sha256()) },
                    AllowedScopes = { "API.Read", "API.Write" }
                }
            ];

        public static IEnumerable<IdentityResource> IdentityResources =>
            [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = ["role"]
                }
            ];

        public static IEnumerable<ApiResource> ApiResources =>
            [
                new ApiResource
                {
                    Name = "ApiBlog",
                    DisplayName = "API Blog",
                    Description = "Allow the application to access API Blog on your behalf",
                    Scopes = ["API.Read", "API.Write"],
                    ApiSecrets = [new Secret("Abc@123".Sha256())],
                    UserClaims = ["role"]
                }
            ];
    }

}
