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

        public async Task<EmployeeData[]> GetEmployees()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "/v0/employees"))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var values = JsonConvert.DeserializeObject<EmployeeRootobject>(await response.Content.ReadAsStringAsync());

                    return values.data;
                }
            }
        }

        public async Task<EmployeeData> GetEmployee(string employeeId)
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "/v0/employees/" + employeeId))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var values = JsonConvert.DeserializeObject<EmployeeData>(await response.Content.ReadAsStringAsync());

                    return values;
                }
            }
        }

        public async Task<Position[]> GetPositions(string employeeId)
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

                    return values.ToArray();
                }
            }
        }

        public async Task<string> CreateEmployee(EmployeeData employee)
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, BASE_URL + "/v0/employees/withPosition"))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    request.Content = new StringContent(JsonConvert.SerializeObject(employee, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode == System.Net.HttpStatusCode.Accepted) //202
                    {
                        return response.Headers.Location.ToString();
                    }
                    else
                        return "Error";
                }
            }
        }

        public async Task<Job> GetJob(string jobPath)
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + jobPath))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var values = JsonConvert.DeserializeObject<Job>(await response.Content.ReadAsStringAsync());

                    return values;
                }
            }
        }
    }
}
