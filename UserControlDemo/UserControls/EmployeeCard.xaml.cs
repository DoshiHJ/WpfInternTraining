using System.Windows;
using System.Windows.Controls;

namespace UserControlDemo.UserControls
{
    public partial class EmployeeCard : UserControl
    {
        public EmployeeCard()
        {
            InitializeComponent();
        }

        // EmployeeName

        public static readonly DependencyProperty EmployeeNameProperty =
            DependencyProperty.Register(
                nameof(EmployeeName),
                typeof(string),
                typeof(EmployeeCard),
                new PropertyMetadata(""));

        public string EmployeeName
        {
            get => (string)GetValue(EmployeeNameProperty);
            set => SetValue(EmployeeNameProperty, value);
        }

        // Department

        public static readonly DependencyProperty DepartmentProperty =
            DependencyProperty.Register(
                nameof(Department),
                typeof(string),
                typeof(EmployeeCard),
                new PropertyMetadata(""));

        public string Department
        {
            get => (string)GetValue(DepartmentProperty);
            set => SetValue(DepartmentProperty, value);
        }

        // Salary

        public static readonly DependencyProperty SalaryProperty =
            DependencyProperty.Register(
                nameof(Salary),
                typeof(string),
                typeof(EmployeeCard),
                new PropertyMetadata(""));

        public string Salary
        {
            get => (string)GetValue(SalaryProperty);
            set => SetValue(SalaryProperty, value);
        }
    }
}