using System;

namespace HRM_API_Demo.APIs.Employee
{
    public class Job
    {
        public string artifactId { get; set; }
        public string artifactType { get; set; }
        public string status { get; set; }
        public int statusCode { get; set; }
        public string action { get; set; }
        public string link { get; set; }
        public DateTime lastChange { get; set; }
        public string message { get; set; }
        public Validationresult[] validationResults { get; set; }
        public string id { get; set; }
        public string tenantId { get; set; }
        public DateTime created { get; set; }
        public Artifact[] artifacts { get; set; }
    }

    public class Validationresult
    {
        public string id { get; set; }
        public string message { get; set; }
        public string messageCode { get; set; }
        public string value { get; set; }
        public string jsonPath { get; set; }
        public string severity { get; set; }
    }

    public class Artifact
    {
        public string artifactId { get; set; }
        public string artifactType { get; set; }
        public string status { get; set; }
        public int statusCode { get; set; }
        public string action { get; set; }
        public string link { get; set; }
        public DateTime lastChange { get; set; }
        public string message { get; set; }
        public Validationresult1[] validationResults { get; set; }
    }

    public class Validationresult1
    {
        public string id { get; set; }
        public string message { get; set; }
        public string messageCode { get; set; }
        public string value { get; set; }
        public string jsonPath { get; set; }
        public string severity { get; set; }
    }

}
