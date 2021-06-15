using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRM_API_Demo.APIs.Employee
{
    public class EmployeeAPI : API
    {
        private const string BASE_URL = "https://employeeapi.employeecore.hrm.visma.net";

        protected override string GetRequiredScopes()
        {
            return "visma_net_employee:employees:read visma_net_employee:employees:write";
        }

        public async Task<EmployeeList> GetEmployees()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "/v0/employees"))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var values = JsonConvert.DeserializeObject<EmployeeList>(await response.Content.ReadAsStringAsync());

                    return values;                    
                }
            }
        }

        public async Task<Employee> GetEmployee(string employeeId)
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "/v0/employees/" + employeeId))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var values = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());

                    return values;
                }
            }
        }

        public async Task<List<Position>> GetPositions(string employeeId)
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "/v0/employees/" + employeeId + "/positions"))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var values = JsonConvert.DeserializeObject<List<Position>>(await response.Content.ReadAsStringAsync());

                    return values;
                }
            }
        }
    }
}
