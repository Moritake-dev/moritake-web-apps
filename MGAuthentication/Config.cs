using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MGAuthentication
{
    public class Config
    {
        // the information resources that we are going to protect
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>()
            {
                // INFO: these scopes are reflected in ID Token
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResource(name: "profile", claimTypes: new []{"name", "email", "firstname", "lastname", "gender"}),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResource
                {
                    Name = "mg.scope",
                    UserClaims =
                    {
                        "test_claim"
                    }
                },
                new IdentityResource
                {
                    Name = "Feature",
                    UserClaims =
                    {
                        "feature_access"
                    }
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(name: ApiConstants.ApiScopes.Read, displayName: "Read data"),
                new ApiScope(name: ApiConstants.ApiScopes.Write, displayName: "Write data"),
                new ApiScope(name: ApiConstants.ApiScopes.ApiFeature, displayName: "Feature of the app"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        // the apis that we are going to protect
        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
                {
                    Name = "MGAuthentication",
                    DisplayName = "MGAuthentication",
                    Description = "Identity server API. Also hosts location API",
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Role
                    },
                    Scopes = { ApiConstants.ApiScopes.Read, ApiConstants.ApiScopes.Write, ApiConstants.ApiScopes.ApiFeature },
                    Enabled = true
                },
                new ApiResource("dokoApi")
                {
                    Name = "dokoApi",
                    DisplayName = "Moritake-gumi Doko Application Api",
                    // INFO: Defining these userclaims will reflect in access_token
                    UserClaims = new List<string>
                    {
                        "test_claim",
                        JwtClaimTypes.Role
                    }
                },

                new ApiResource("mg.admin")
                {
                    Name = "mg.admin",
                    DisplayName = "Moritake-gumi Administration API",
                    // INFO: Defining these userclaims will reflect in access_token

                    UserClaims = new List<string>
                    {
                        "feature_access",
                        JwtClaimTypes.Role
                    },
                    Scopes = { "read", "write", "FeatureScope" }
                },

                new ApiResource("mg.reportManagement")
                {
                    Name = "mg.Nippo",
                    DisplayName = "Moritake-gumi Report Management API",

                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Role
                    },
                    Scopes = { "read", "write", "FeatureScope" }
                }
            };

        // clients that are going to connect to this server
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    // clientId and secrets of the client application
                    ClientId = "doko.ui",
                    ClientSecrets = {new Secret("doko_ui_secret".Sha256())},

                    // grant-type and consents.
                    // offline access is used for refresh token
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    // includes all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,

                    // redirectUri is a required field. this redirect uri is got by the openidmodel package
                    RedirectUris = {"https://localhost:44392/signin-oidc"},

                    // allowed scopes of this client
                    AllowedScopes =
                    {
                        "Feature",
                        "dokoApi",
                        "mg.scope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },

                new Client
                {
                    ClientId = "mg.administration",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("mg.administration_secret".Sha256())
                    },

                    AllowedScopes =
                    {
                        "Feature",
                        "FeatureScope",
                        "mg.admin",
                        "dokoApi",
                        "mg.scope",
                        "read",
                        "write",
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },

                new Client
                {
                    ClientId = ApiConstants.Clients.MGSolutions,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,

                    // this is the PKCE type authentication. basically it is the same principal as GrantTypes.Code
                    // we do not have client secret in javascript client that is why we generate a temporary secret and send it to server
                    // server than hash it and compare with the temporary secret that client has sent again.
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:4200/" },

                    AllowedCorsOrigins = new List<string> { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        ApiConstants.ApiScopes.Read,
                        ApiConstants.ApiScopes.Write,
                        ApiConstants.ApiScopes.ApiFeature,
                        "dokoApi",
                        "mg.scope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },
            };
    }
}