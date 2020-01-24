using System.Collections.Generic;

namespace EmployeeManager
{
    public class Employee
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginID { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Access { get; set; }


        public Employee(string ID, string FirstName, string LastName, string LoginID, string DOB, string Email, string Phone, string Access)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.LoginID = LoginID;
            this.DOB = DOB;
            this.Email = Email;
            this.Phone = Phone;
            this.Access = Access;
        }


        public static void MakeEmployee(string ID, string FirstName, string LastName, string LoginID, string DOB, string Email, string Phone, string Access)
        {
            AddEmployee( new Employee(ID, FirstName, LastName, LoginID, DOB, Email, Phone, Access) );
        }


        public static void AddEmployee(Employee employee)
        {
            Database.Write(Constants.Path, employee);
        }


        public static void EditEmployee(int position, Employee newEmployee)
        {
            var employees = Database.Read();

            employees.RemoveAt(position - 1);

            employees.Insert(position - 1, newEmployee);

            Database.Delete(Constants.Path);

            foreach (var employee in employees)
            {
                Database.Write(Constants.Path, employee);
            }
        }
       

        public static void DeleteEmployee(int position)
        {
            var employees = Database.Read();

            employees.RemoveAt(position - 1);

            Database.Delete(Constants.Path);

            foreach (var employee in employees)
            {
                Database.Write(Constants.Path, employee);
            }
        }


        public static string[] GetEmployeeData(Employee employee)
        {
            string[] data = new string[] {  employee.FirstName,
                                            employee.LastName,
                                            employee.ID,
                                            employee.DOB,
                                            employee.LoginID,
                                            employee.Email,
                                            employee.Phone,
                                            employee.Access };
            return data;
        }


        public static int GetDatabasePosition(string ID)
        {
            List<string> idList = new List<string>();

            var employees = Database.Read();

            foreach (var employee in employees)
            {
                idList.Add(employee.ID);
            }

            return idList.IndexOf(ID) + 1;
        }

    }
}
