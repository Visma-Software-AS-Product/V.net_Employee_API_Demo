using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_API_Demo.APIs.Employee
{
    public class Position
    {
        
        [JsonProperty("activeStart")]
        public string ActiveStart { get; set; }

        [JsonProperty("activeEnd")]
        public string ActiveEnd { get; set; }

        [JsonProperty("typeOfPosition")]
        public string TypeOfPosition { get; set; }

    }
}
