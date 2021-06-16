using HRM_API_Demo.APIs.Employee;
using System;
using System.Linq;
using System.Threading;

namespace HRM_API_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        private static void ShowMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Visma.net Payroll API Demo Application");
            Console.WriteLine("*****************************************************");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Choose one of the following actions by pressing the number:");
            Console.WriteLine("1: Get a list of employees");
            Console.WriteLine("2: Get a single employee with positions");
            Console.WriteLine("3: Create a new employee");
            Console.WriteLine("4: Create a new employee (gives error)");
            Console.WriteLine();
            Console.WriteLine("Please choose (1-5):");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ListEmployees();
                    break;
                case "2":
                    Console.Clear();
                    GetEmployee();
                    break;
                case "3":
                    Console.Clear();
                    CreateEmployee(true);
                    break;
                case "4":
                    Console.Clear();
                    CreateEmployee(false);
                    break;
                default:
                    Console.WriteLine("Unknown input");
                    break;
            }
        }

        private static void ListEmployees()
        {
            APIs.Employee.EmployeeAPI empAPI = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Getting a list of employees");

            var emplist = empAPI.GetEmployees().GetAwaiter().GetResult();

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine();
            Console.WriteLine("Firstname      LastName       Date of Birth");
            Console.WriteLine("-------------- -------------- -------------");

            foreach (var employee in emplist)
            {
                Console.WriteLine("{0}{1}{2}", 
                    employee.personNames[0].firstName.PadRight(15, ' '), 
                    employee.personNames[0].lastName.PadRight(15, ' '), 
                    employee.dateOfBirth);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu");

            Console.ReadLine();

            ShowMenu();
        }

        private static void GetEmployee()
        {
            APIs.Employee.EmployeeAPI empAPI = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Getting a single employee based on Id and listing all positions");

            var singleemployee = empAPI.GetEmployee("d032f491-e834-4844-914c-f03c0d82e278").GetAwaiter().GetResult();
            var positions = empAPI.GetPositions("d032f491-e834-4844-914c-f03c0d82e278").GetAwaiter().GetResult();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Employee: {0} {1}", singleemployee.personNames[0].firstName, singleemployee.personNames[0].lastName);

            Console.WriteLine();
            Console.WriteLine("Type of position Employmentform Start date Salarytype Salary");
            Console.WriteLine("---------------- -------------- ---------- ---------- ------");

            foreach (var pos in positions)
            {
                Console.WriteLine("{0}{1}{2}{3}{4}", 
                    pos.typeOfPosition.PadRight(17, ' '),
                    pos.employmentForm[0].value.PadRight(15, ' '), 
                    pos.activeStart.PadRight(11, ' '), 
                    pos.salaryInformation[0].salaryType.PadRight(11, ' '),
                    pos.salaryInformation[0].monthlySalary.ToString());
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            Console.WriteLine("Press any key to return to main menu");

            Console.ReadLine();

            ShowMenu();
        }

        private static void CreateEmployee(bool includeTestData)
        {
            Console.Clear();

            APIs.Employee.EmployeeAPI empAPI = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Create a new employee");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Employeenumber: ");
            var employeeNumber = Console.ReadLine();

            Console.Write("Firstname: ");
            var firstname = Console.ReadLine();

            Console.Write("Lastname: ");
            var lastname = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Creating {0} {1}...", firstname, lastname);

            EmployeeData employeeData = new EmployeeData();
            employeeData.personNames = new[] { new Personname() { firstName = firstname, lastName = lastname } };
            employeeData.number = employeeNumber;

            if(includeTestData)
            { 
                employeeData.printPayslip = false;
                employeeData.personIds = new[] { new Personid() { typeOfId = "NationalID", id = RandomPersonId.GetRandomPersonId() } };
                employeeData.salaryPaymentMethod = new Salarypaymentmethod() { paymentType = "bank", localBankAccount = "12345678903" };
                employeeData.positions = new[] { new Position() { activeStart = "2021-06-01", 
                                                                  typeOfPosition = "Ordinary", 
                                                                  taxUnitIds = new[] { new Taxunitid() { value = "2" } },
                                                                  partTimeFactors = new[] { new Parttimefactors() { value = 100} },
                                                                  salaryInformation = new[] { new Salaryinformation() { salaryType = "Period", monthlySalary = "1000"} },
                                                                  typeOfWork = new[] { new Typeofwork() { value = "1233101" } },
                                                                  workTimeAgreement = new[] { new Worktimeagreement() {  value = "0387f476-2471-4816-8b94-cdabb8fe4c21" } },
                                                                  employmentForm = new[] { new Employmentform() { value = "fast" } }
                                                                  }
                                                 };
            }

            var jobpath = empAPI.CreateEmployee(employeeData).GetAwaiter().GetResult();

            Console.WriteLine("Job created. Waiting for task to finish...");

            Job job = null;

            while (job == null || job.status == "NotStarted" || job.status == "InProgress")
            {
                job = empAPI.GetJob(jobpath).GetAwaiter().GetResult();
                Thread.Sleep(3000);
            }

            if(job.status == "Failed") 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Job failed: {0}", job.message);
                Console.WriteLine();
            }
            else if(job.status == "Succeeded")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Employee created");      
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu");

            Console.ReadLine();

            ShowMenu();
        }
    }
}
