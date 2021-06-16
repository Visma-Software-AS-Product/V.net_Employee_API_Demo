namespace HRM_API_Demo.APIs.Employee
{
    public class Position
    {
        public string id { get; set; }
        public string tenantId { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
        public string employeeId { get; set; }
        public string typeOfPosition { get; set; }
        public Employmentform[] employmentForm { get; set; }
        public Typeofwork[] typeOfWork { get; set; }
        public Worktimeagreement[] workTimeAgreement { get; set; }
        public Parttimefactors[] partTimeFactors { get; set; }
        public Salaryinformation[] salaryInformation { get; set; }
        public Taxunitid[] taxUnitIds { get; set; }
        public string[] positionEndReason { get; set; }
        public Externalid[] externalIds { get; set; }
    }

    public class Employmentform
    {
        public string value { get; set; }
    }

    public class Worktimeagreement
    {
        public string value { get; set; }
    }

    public class Parttimefactors
    {
        public int value { get; set; }
    }

    public class Typeofwork
    {
        public string value { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Salaryinformation
    {
        public string salaryType { get; set; }
        public string monthlySalary { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Taxunitid
    {
        public string value { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Externalid
    {
        public string key { get; set; }
        public string value { get; set; }
    }

}
