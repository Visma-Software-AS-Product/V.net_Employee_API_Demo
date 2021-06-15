using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_API_Demo.APIs.Employee
{
    public class EmployeeList
    {
        [JsonProperty("data")]
        public List<Employee> Employees { get; set; }
    }

    public class PersonName
    {
        [JsonProperty("firstName")]

        public string FirstName { get; set; }

        [JsonProperty("lastName")]

        public string lastName { get; set; }

    }

    public class Employee
    {
        [JsonProperty("personNames")]

        public List<PersonName> personNames { get; set; }
        
        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }
    }
}
