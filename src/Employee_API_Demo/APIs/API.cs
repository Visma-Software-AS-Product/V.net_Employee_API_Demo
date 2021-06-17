using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRM_API_Demo.APIs
{
    /// <summary>
    /// Base-class for API-wrapper classes. Since all HRM-APIs uses Visma Connect for authentication the logic for getting the token is placed here
    /// </summary>
    public abstract class API
    {
        /// <summary>
        /// All API-wrapper classes must contain information about the API-Scopes they require
        /// </summary>
        /// <returns>A string including the required scopes (separated by [space])</returns>
        protected abstract string GetRequiredScopes();

        /// <summary>
        /// Collects a new token from Visma Connect.
        /// </summary>
        /// <returns>The token</returns>
        protected async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://connect.visma.com/connect/token"))
                {
                    // The token-request requires the grant_type (always client_credentials), the ClientId/Secret a tenantId and the scopes to be provided as FormUrlEncoded content in the request. 
                    request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                        { "grant_type", "client_credentials" },
                        { "client_id", Environment.GetEnvironmentVariable("ClientId")},
                        { "client_secret", Environment.GetEnvironmentVariable("ClientSecret") },
                        { "tenant_id", Environment.GetEnvironmentVariable("TenantId") }, // Since the APIs are tenant-enabled, a valid tenantId must be provided to obtain a token.
                        { "scope", GetRequiredScopes() }
                    });

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var payload = Newtonsoft.Json.Linq.JObject.Parse(await response.Content.ReadAsStringAsync());
                    var token = payload.Value<string>("access_token");

                    // Returns the created token
                    return token;

                    //NB! The token is valid for 3600 seconds (1 hour), so it should be reused for this time. This is not implemented in this test.
                }
            }
        }
    }
}
