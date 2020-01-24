using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManager
{
    public class Database
    {
        private static void WriteHeader(string path)
        {
            string header = "ID,First,Last,Login,DOB,Email,Phone,Access\n";

            try
            {
                System.IO.File.WriteAllText(path, header, Encoding.UTF8);
            }

            catch (System.IO.IOException)
            {
                // Bad Stuff Happened
            }
        }


        public static void Write(string path, Employee employee)
        {
            if (!System.IO.File.Exists(path))
            {
                WriteHeader(path);
            }

            string data =   employee.ID + "," +
                            employee.FirstName + "," +
                            employee.LastName + "," +
                            employee.LoginID + "," +
                            employee.DOB + "," +
                            employee.Email + "," +
                            employee.Phone + "," +
                            employee.Access + "\n";

            try
            {
                System.IO.File.AppendAllText(path, data, Encoding.UTF8);
            }

            catch (System.IO.IOException)
            {
                // Bad Stuff Happened
            }
        }


        public static List<Employee> Read()
        {
            return Read(Constants.Path);
        }


        public static List<Employee> Read(string path)
        {
            List<Employee> employees = new List<Employee>();

            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path, Encoding.UTF8);

                foreach (string line in lines.Skip(1))
                {
                    string[] values = line.Split(',');

                    if (values.GetLength(0) == 8)
                    {
                        employees.Add(new Employee(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]));
                    }
                }
            }

            return employees;
        }


        public static void Delete(string path)
        {
            System.IO.File.Delete(path);
        }

    }
}
