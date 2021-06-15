using System;
using System.Linq;

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
            Console.WriteLine("4: Update an employee");
            Console.WriteLine("5: Delete an employee");
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

            foreach (var employee in emplist.Employees)
            {
                Console.WriteLine("{0}{1}{2}", 
                    employee.personNames[0].FirstName.PadRight(15, ' '), 
                    employee.personNames[0].lastName.PadRight(15, ' '), 
                    employee.DateOfBirth);
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
            Console.WriteLine("Employee: {0} {1}", singleemployee.personNames[0].FirstName, singleemployee.personNames[0].lastName);

            Console.WriteLine();
            Console.WriteLine("Type of position Start date End date");
            Console.WriteLine("---------------- ---------- ----------");

            foreach (var pos in positions)
            {
                Console.WriteLine("{0}{1}{2}", 
                    pos.TypeOfPosition.PadRight(17, ' '), 
                    pos.ActiveStart.PadRight(11, ' '), 
                    pos.ActiveEnd);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            Console.WriteLine("Press any key to return to main menu");

            Console.ReadLine();

            ShowMenu();
        }
    }
}
