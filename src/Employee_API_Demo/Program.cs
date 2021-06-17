using HRM_API_Demo.APIs.Employee;
using System;
using System.Threading;

namespace HRM_API_Demo
{
    /// <summary>
    /// Console-application to run the tests. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        /// <summary>
        /// Shows the main menu for the test-application
        /// </summary>
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

        /// <summary>
        /// Executes the test to get multiple Employees from the API
        /// </summary>
        private static void ListEmployees()
        {
            // Creates an instance of the EmployeeAPI-wrapper
            APIs.Employee.EmployeeAPI empAPI = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Getting a list of employees");

            // Makes the call to get a list of employees from the API. 
            // The communication to the API is async, but in this case we want to wait for the task to finish before continuing.
            var emplist = empAPI.GetEmployees().GetAwaiter().GetResult();

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine();
            Console.WriteLine("Firstname      LastName       Date of Birth");
            Console.WriteLine("-------------- -------------- -------------");

            // Loops through all the returned employees and print the result
            foreach (var employee in emplist)
            {
                Console.WriteLine("{0}{1}{2}", 
                    employee.personNames[0].firstName.PadRight(15, ' '),  // An employee can have multiple names, for simplicity we choose the first one.
                    employee.personNames[0].lastName.PadRight(15, ' '), 
                    employee.dateOfBirth);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu");

            Console.ReadLine();

            ShowMenu();
        }

        /// <summary>
        /// Executes the test to get a single employee, including the positions for this employee from the API
        /// </summary>
        private static void GetEmployee()
        {
            // Creates an instance of the EmployeeAPI-wrapper
            APIs.Employee.EmployeeAPI empAPI = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Getting a single employee based on Id and listing all positions");

            // Makes the call to get a single of employees from the API. For simplicity we use a hardcoded EmployeeID. 
            // The communication to the API is async, but in this case we want to wait for the task to finish before continuing.
            var singleemployee = empAPI.GetEmployee("d032f491-e834-4844-914c-f03c0d82e278").GetAwaiter().GetResult();

            // Makes the call to get a list of all the positions for the employees from the API. For simplicity we use a hardcoded EmployeeID. 
            // The communication to the API is async, but in this case we want to wait for the task to finish before continuing.
            var positions = empAPI.GetPositions("d032f491-e834-4844-914c-f03c0d82e278").GetAwaiter().GetResult();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // Writes out the name of the employee. An employee can have multiple names, for simplicity we choose the first one.
            Console.WriteLine("Employee: {0} {1}", singleemployee.personNames[0].firstName, singleemployee.personNames[0].lastName);

            Console.WriteLine();
            Console.WriteLine("Type of position Employmentform Start date Salarytype Salary");
            Console.WriteLine("---------------- -------------- ---------- ---------- ------");

            // Loops through all the returned positions and print the results.
            // For the properties where there can be more than result (empolymentform, salaryinformation) we choose the first one for simplicity.
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

        /// <summary>
        /// Executes the test to create a new employee with a position via the API.
        /// </summary>
        /// <param name="includeTestData">Creating an employee with a position have some required fields, set this parameter to TRUE to insert testdata for these fields.</param>
        private static void CreateEmployee(bool includeTestData)
        {
            // Creates an instance of the EmployeeAPI-wrapper
            APIs.Employee.EmployeeAPI empAPI = new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Create a new employee");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Employeenumber: ");
            var employeeNumber = Console.ReadLine(); // Collects the employeenumber from the user.

            Console.Write("Firstname: ");
            var firstname = Console.ReadLine(); // Collects the employeenumber from the user.

            Console.Write("Lastname: ");
            var lastname = Console.ReadLine(); // Collects the employeenumber from the user.

            Console.WriteLine();

            Console.WriteLine("Creating {0} {1}...", firstname, lastname);

            // Creates an instance of the EmployeeData-class. This class is generated by Visual Studio based on the Swagger-documentation for the API.
            EmployeeData employeeData = new EmployeeData();
            employeeData.personNames = new[] { new Personname() { firstName = firstname, lastName = lastname } }; // Creates the name-instance.
            employeeData.number = employeeNumber; // Inserts the employee-number.

            // If the option to insert static test-data has been chosen, we append data in the required fields
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

            // Makes the call to create the employee via the API. 
            // The communication to the API is async, but in this case we want to wait for the task to finish before continuing.
            // The task of creating a new employee is run asynchronous on the server-side. Therefore the returned value is a path to check the status of the creation-task.
            var jobpath = empAPI.CreateEmployee(employeeData).GetAwaiter().GetResult();

            Console.WriteLine("Job created. Waiting for task to finish...");

            Job job = null;

            // While waiting for the job (create new employee) to be completed by the server we make calls to the server every 3 seconds to check the status of the job.
            while (job == null || job.status == "NotStarted" || job.status == "InProgress")
            {
                // Calls the API to get the status of the job.
                // The communication to the API is async, but in this case we want to wait for the task to finish before continuing.
                job = empAPI.GetJob(jobpath).GetAwaiter().GetResult();
                Thread.Sleep(3000);
            }

            // If the job completes with a "Failed"-status we write out the error-messages to the screen.
            if(job.status == "Failed") 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Job failed: {0}", job.message);
                Console.WriteLine();
            }
            // If the job completes with a "Succeeded"-status we write out a message to the screen.
            else if (job.status == "Succeeded")
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
