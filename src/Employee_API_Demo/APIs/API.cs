using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRM_API_Demo.APIs
{
    public abstract class API
    {
        protected abstract string GetRequiredScopes();

        protected async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://connect.visma.com/connect/token"))
                {
                    request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                        { "grant_type", "client_credentials" },
                        { "client_id", Environment.GetEnvironmentVariable("ClientId")},
                        { "client_secret", Environment.GetEnvironmentVariable("ClientSecret") },
                        { "tenant_id", Environment.GetEnvironmentVariable("TenantId") },
                        { "scope", GetRequiredScopes() }
                    });

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var payload = Newtonsoft.Json.Linq.JObject.Parse(await response.Content.ReadAsStringAsync());
                    var token = payload.Value<string>("access_token");

                    return token;
                }
            }
        }
    }
}
