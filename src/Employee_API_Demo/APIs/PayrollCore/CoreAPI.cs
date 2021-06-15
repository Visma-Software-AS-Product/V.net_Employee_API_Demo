using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRM_API_Demo.APIs.PayrollCore
{
    public class CoreAPI : API
    {
        private const string BASE_URL = "https://api.payroll.core.hrm.visma.net";
        protected override string GetRequiredScopes()
        {
            return "payroll:employees:read";
        }

        public async Task<int> GetNumberOfEmployees()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "/v1/query/employees"))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var payload = Newtonsoft.Json.Linq.JObject.Parse(await response.Content.ReadAsStringAsync());

                    Newtonsoft.Json.Linq.JArray employees = (Newtonsoft.Json.Linq.JArray)payload["data"];

                    return employees.Count;
                }
            }
        }
    }
}
