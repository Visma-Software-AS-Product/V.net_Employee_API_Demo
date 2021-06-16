namespace HRM_API_Demo.APIs.Employee
{
    public class EmployeeRootobject
    {
        public Cursor cursor { get; set; }
        public EmployeeData[] data { get; set; }
    }

    public class Cursor
    {
        public string nextToken { get; set; }
    }

    public class EmployeeData
    {
        public string id { get; set; }
        public string number { get; set; }
        public string tenantId { get; set; }
        public bool isDraft { get; set; }
        public Personname[] personNames { get; set; }
        public Personid[] personIds { get; set; }
        public string dateOfBirth { get; set; }
        public Phone[] phones { get; set; }
        public Email[] emails { get; set; }
        public Address address { get; set; }
        public Relative[] relatives { get; set; }
        public Salarypaymentmethod salaryPaymentMethod { get; set; }
        public Taxinformation taxInformation { get; set; }
        public Sicknessinformation sicknessInformation { get; set; }
        public Pensioncompanyid[] pensionCompanyIds { get; set; }
        public Union[] unions { get; set; }
        public bool printPayslip { get; set; }
        public Externalid[] externalIds { get; set; }
        public Position[] positions { get; set; }
    }

    public class Address
    {
        public string streetName { get; set; }
        public string cityName { get; set; }
        public string zipCode { get; set; }
        public string countryCode { get; set; }
    }

    public class Salarypaymentmethod
    {
        public string paymentType { get; set; }
        public bool internationalBankAccount { get; set; }
        public string localBankAccount { get; set; }
        public string internationalBankSwift { get; set; }
        public string internationalIban { get; set; }
        public string internationalBankCountry { get; set; }
        public string internationalRemittanceCountry { get; set; }
    }

    public class Taxinformation
    {
        public bool mainEmployer { get; set; }
    }

    public class Sicknessinformation
    {
        public Chronicallyillvalue[] chronicallyIllValues { get; set; }
    }

    public class Chronicallyillvalue
    {
        public bool value { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Personname
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Personid
    {
        public string typeOfId { get; set; }
        public string typeOfInternationalId { get; set; }
        public string id { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Phone
    {
        public string type { get; set; }
        public int number { get; set; }
    }

    public class Email
    {
        public string type { get; set; }
        public string address { get; set; }
    }

    public class Relative
    {
        public string type { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public string dateOfBirth { get; set; }
        public bool chronicallyIll { get; set; }
        public bool soleCustody { get; set; }
    }

    public class Pensioncompanyid
    {
        public string value { get; set; }
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
    }

    public class Union
    {
        public string activeStart { get; set; }
        public string activeEnd { get; set; }
        public string unionId { get; set; }
        public string unionMemberNumber { get; set; }
        public bool includedInCollectiveInsurance { get; set; }
        public bool includedInUnionsInsurance { get; set; }
        public int employeeAmountForCollectiveInsurance { get; set; }
        public int employeeAmountForUnionsInsurance { get; set; }
    }

    //public class CreateEmployeeData
    //{
    //    public string number { get; set; }
    //    public Personname[] personNames { get; set; }

    //    public bool printPayslip { get; set; }
    //}

    //public class CreatePersonname
    //{
    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //}

}
