using System.Collections.ObjectModel;
using UserControlDemo.Models;

namespace UserControlDemo.ViewModels
{
    public class EmployeeViewModel
    {
        public ObservableCollection<Employee> Employees
        {
            get;
            set;
        }

        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>();

            Employees.Add(new Employee
            {
                Name = "Harsh",
                Department = "IT",
                Salary = "₹50,000"
            });

            Employees.Add(new Employee
            {
                Name = "Rahul",
                Department = "HR",
                Salary = "₹40,000"
            });

            Employees.Add(new Employee
            {
                Name = "Amit",
                Department = "Finance",
                Salary = "₹60,000"
            });

            Employees.Add(new Employee
            {
                Name = "krish",
                Department = "Seo",
                Salary = "₹30,000"
            });
        }
    }
}