using System.Collections.Generic;
using System.Windows;

namespace EmployeeManager
{
    public partial class InputWindow : Window
    {
        private string EmployeeID;
        private int DatabasePosition;
        private bool edit = false;


        public InputWindow()
        {
            InitializeComponent();

            inputFirst.Focus();
        }


        public InputWindow(int databasePosition, string id) : this()
        {
            EmployeeID = id;
            DatabasePosition = databasePosition;
            edit = true;
        }


        private void Button_OK(object sender, RoutedEventArgs e)
        {
            if ( string.IsNullOrEmpty(inputFirst.Text) )
            { 
                Close();

                return;
            }

            var employees = Database.Read();
       
            List<string> idList = new List<string>();

            foreach (var employee in employees)
            {
                idList.Add(employee.ID);
            }

            int ID = 0;

            for (int i = 1; i <= idList.Count + 1; i++)
            {
                if ( !idList.Contains(i.ToString() ) )
                {
                    ID = i;

                    break;
                }
            }
                
            // Edit Employee
            if (edit)
            {
                Employee newEmployee = new Employee(EmployeeID,
                                    inputFirst.Text,
                                    inputLast.Text,
                                    inputFirst.Text.Substring(0, 1) + inputLast.Text,
                                    inputDOB.Text,
                                    inputEmail.Text,
                                    inputPhone.Text,
                                    inputAccess.Text);

                Employee.EditEmployee(DatabasePosition, newEmployee);
            }

            // Add Employee
            else
            {
                Employee newEmployee = new Employee(ID.ToString(),
                                                    inputFirst.Text,
                                                    inputLast.Text,
                                                    inputFirst.Text.Substring(0, 1) + inputLast.Text,
                                                    inputDOB.Text,
                                                    inputEmail.Text,
                                                    inputPhone.Text,
                                                    inputAccess.Text);

                Employee.AddEmployee(newEmployee);
            }

            Close();
        }


        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
