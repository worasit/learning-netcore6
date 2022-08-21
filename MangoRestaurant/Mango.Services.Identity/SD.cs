using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity;

public static class SD
{
    public static class Roles
    {
        /// <summary>
        /// Admin role
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// Customer role
        /// </summary>
        public const string Customer = "Customer";
    }


    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope(name: "mango", displayName: "Mango Server"),
            new ApiScope(name: "read", displayName: "Read your data"),
            new ApiScope(name: "write", displayName: "Write your data"),
            new ApiScope(name: "delete", displayName: "Delete your data")
        };

    public static IEnumerable<Client> Clients => new[]
    {
        new Client
        {
            ClientId = "client",
            ClientSecrets = {new Secret("secret".Sha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"read", "write", "delete", "profile"}
        },
        new Client
        {
            ClientId = "mango",
            ClientSecrets = {new Secret("secret".Sha256())},
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = {"https://localhost:7163/signin-oidc"},
            PostLogoutRedirectUris = {"https://localhost:7163/signout-callback-oidc"},
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "mango"
            }
        }
    };
}