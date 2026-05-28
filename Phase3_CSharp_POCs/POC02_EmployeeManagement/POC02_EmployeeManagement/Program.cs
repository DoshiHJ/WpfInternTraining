using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC02_EmployeeManagement
{
    class employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string department {  get; set; }
        public decimal salary {  get; set; }
       
        public DateTime joiningdate { get; set; }
    
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<employee> employees = new List<employee>()
            {
                new employee
                {
                    Id = 1, Name = "harsh", department = "software developer", salary = 50000, joiningdate = new DateTime(2008, 11,15)
                },

                new employee
                {
                    Id = 2, Name = "krish", department = "seo engineer", salary = 30000, joiningdate = new DateTime(2026, 2, 10)
                },

                new employee
                {
                    Id = 3, Name = "nanu", department = "desktop developer", salary = 60000, joiningdate = new DateTime(2026, 1, 20) 
                }

               
            };

            string choice;

            do
            {
                Console.WriteLine("\n === Employee Management System ===");
                Console.WriteLine(" 1. Add New Employee \n 2. View All Employees \n 3. Search Employee by ID \n 4. Update Employee Details \n 5. Delete Employee \n 6. Exit");

                Console.WriteLine("\n Choose option:");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter Id:");
                        int id=int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Name:");
                        string name=Console.ReadLine();
                       
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            do
                            {
                                Console.WriteLine("Name cannot be empty");
                                Console.WriteLine("Enter Name:");
                                name = Console.ReadLine();
                            }
                            while (string.IsNullOrWhiteSpace(name));
                        }
                        Console.WriteLine("Enter department:");
                        string department = Console.ReadLine();
                        Console.WriteLine("Enter salary:");
                        decimal salary=decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter DOJ:");
                        DateTime joiningdate = DateTime.Parse(Console.ReadLine());

                        employees.Add(
                        new employee
                        {
                            Id = id,
                            Name = name,
                            department = department,
                            salary = salary,
                            joiningdate = joiningdate

                        });
                        break;

                    case "2":
                        Console.WriteLine("\n=== All Employees ===\n");

                        Console.WriteLine(
                            "{0,-1} {1,-10} {2,-20} {3,-10} {4,-15}",
                            "ID",
                            "Name",
                            "Department",
                            "Salary",
                            "Joining Date"
                        );

                        Console.WriteLine(new string('-', 75));

                        foreach (var emp in employees)
                        {
                            Console.WriteLine(
                                "{0,-1} {1,-10} {2,-20} {3,-10} {4,-15:yyyy-MM-dd}",
                                emp.Id,
                                emp.Name,
                                emp.department,
                                emp.salary,
                                emp.joiningdate
                            );
                        }

                        Console.WriteLine(new string('-', 75));

                        break;

                    case "3":
                        Console.WriteLine("Search Employee By ID: ");
                        int sid=int.Parse(Console.ReadLine());

                        var employeeData = employees.FirstOrDefault(x => x.Id == sid);

                        Console.WriteLine($"Id:{employeeData.Id}, Name:{employeeData.Name}, Department:{employeeData.department}, Salary:{employeeData.salary}, DOJ:{employeeData.joiningdate}");

                        break;

                    case "4":
                        update();
                        break;

                    case "5":
                        Console.WriteLine("Enter Id You want to Delete it:");
                        int did = int .Parse(Console.ReadLine());
                        
                        var result=employees.FirstOrDefault(x => x.Id == did);

                        employees.RemoveAt(employees.IndexOf(result));

                        break;

                       
                }
            }
            while (choice != "6");

            void update()
            {
                Console.WriteLine("Search Employee By ID: ");
                int uid = int.Parse(Console.ReadLine());

                var uemployee = employees.FirstOrDefault(x => x.Id == uid);

                Console.WriteLine($"id:{uemployee.Id} Name:{uemployee.Name} Department:{uemployee.department} Salary:{uemployee.salary} DOJ:{uemployee.joiningdate}");

                Console.WriteLine("\n Enter Field you want to edit:");
                string field = Console.ReadLine();

                switch (field.ToLower())
                {
                    case "name":
                        string uname;
                        Console.WriteLine("Enter updated name:");
                        uname = Console.ReadLine();
                       
                            if (string.IsNullOrWhiteSpace(uname))
                            {
                                do
                                {
                                Console.WriteLine("Name cannot be empty");
                                Console.WriteLine("Enter updated name:");
                                    uname = Console.ReadLine();
                                }
                                while (string.IsNullOrWhiteSpace(uname));

                            }
                        
                       
                        uemployee.Name = uname;
                        Console.WriteLine("your new name is" + uemployee.Name);
                        break;

                    case "department":
                        Console.WriteLine("Enter updated department:");
                        string udept = Console.ReadLine();
                        uemployee.department = udept;
                        Console.WriteLine("your new name is" + uemployee.department);
                        break;

                    case "salary":
                        Console.WriteLine("Enter updated salary:");
                        decimal usal = Convert.ToDecimal(Console.ReadLine());
                        uemployee.salary = usal;
                        Console.WriteLine("your new name is" + uemployee.salary);
                        break;

                    case "DOJ":
                        Console.WriteLine("Enter updated doj:");
                        DateTime udoj = Convert.ToDateTime(Console.ReadLine());
                        uemployee.joiningdate = udoj;
                        Console.WriteLine("your new name is" + uemployee.joiningdate);
                        break;

                    default:
                        Console.WriteLine("Enter valid field");
                        break;
                }
            }

        }
    }
}
