using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;
using AdaCredit.UI.UseCases;

namespace AdaCredit.UI
{
    public class Login
    {
        private static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Employees", "Employees.csv");
        private readonly EmployeeRepository _employeeRepository;

        public Login(EmployeeRepository _employeeRepository)
        {
            this._employeeRepository = _employeeRepository;
        }
        public bool Show()
        {
            var loggedIn = false;

            do
            {
                Console.Clear();
                string[] lines = File.ReadAllLines(_filePath);
                if (lines.Length == 0)
                {
                    Console.Write("Digite o Nome de Usuário: ");
                    var username = Console.ReadLine();

                    Console.Write("Digite a Senha: ");
                    var password = Console.ReadLine();

                    loggedIn = username.Equals("user", StringComparison.InvariantCultureIgnoreCase)
                                                && password.Equals("pass", StringComparison.InvariantCultureIgnoreCase);
                }
                else
                {
                    Console.Write("Digite o Nome de Usuário: ");
                    var username = Console.ReadLine();
                    loggedIn = EmployeeLogin.Login(username);
                }


            } while (!loggedIn);

            Console.Clear();
            Console.WriteLine("Login Efetuado com Sucesso!");
            Console.WriteLine("<pressione qualquer tecla para continuar>");
            Console.ReadKey();
            
            Console.Clear();

            return true;
        }
    }
}