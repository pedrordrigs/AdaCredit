using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdaCredit.UI.Entities;
using AdaCredit.UI.UseCases;

namespace AdaCredit.UI.Repositories
{
    public class EmployeeRepository
    {
        private static List<Employee> _employees = new List<Employee>();
        private static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Employees", "Employees.csv");
        static EmployeeRepository()
        {
            try
            {
                // Read the file and store the contents in the _employees list
                using (var reader = new StreamReader(_filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var name = values[0];
                        var login = values[1];
                        var password = values[2];
                        bool status = bool.Parse(values[3]);
                        var employee = new Employee(name, login, password, status);
                        _employees.Add(employee);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool Add(Employee employee)
        {
            if (_employees.Any(x => x.Login.Equals(employee.Login)))
            {
                Console.WriteLine("Funcionário já cadastrado");
                Console.ReadKey();

                return false;
            }

            var password = employee.Password;
            var newPass = PasswordHandler.Encrypt(password);

            employee.Password = newPass;

            var entity = new Employee(employee.Name, employee.Login, employee.Password);
            _employees.Add(entity);

            Save();

            return true;
        }

        public bool ChangePassword(string username)
        {
            var employee = _employees.SingleOrDefault(x => x.Login.Equals(username));

            if (employee == null)
            {
                return false;
            }

            var hashedPassword = employee.Password;

            Console.WriteLine("Digite a senha atual:");
            string password = Console.ReadLine();

            bool passwordMatch = PasswordHandler.Decrypt(password, hashedPassword);

            if(!passwordMatch)
                return false;

            Console.WriteLine("Digite a nova senha:");
            string newPassword = Console.ReadLine();
            var newPass = PasswordHandler.Encrypt(newPassword);

            employee.Password = newPass;
            Save();
            return true;
        }

        public bool Deactivate(string user)
        {
            var repository = new EmployeeRepository();
            var employee = _employees.SingleOrDefault(x => x.Login.Equals(user));
            if (employee == null)
            {
                return false;
            }
            employee.Deactivate();
            repository.Save();
            return true;
        }

        public static void GetLastLogin()
        {
            foreach (var employee in _employees)
            {
                if (employee.isActive)
                {
                    Console.WriteLine($"Nome: {employee.Name}");
                    Console.WriteLine($"Último Login: {employee.lastLogin}");

                }
            }
        }

        public Employee CheckUsername(string username)
        {
            var repository = new ClientRepository();
            var employee = _employees.SingleOrDefault(x => x.Login.Equals(username));
            if (employee == null)
            {
                return null;
            }
            else
                return employee;
        }


        public void Save()
        {
            using (var writer = new StreamWriter(_filePath))
            {
                foreach (var employee in _employees)
                {
                    writer.WriteLine($"{employee.Name},{employee.Login},{employee.Password},{employee.isActive}, {employee.lastLogin}");
                }
            }
        }
    }
}
