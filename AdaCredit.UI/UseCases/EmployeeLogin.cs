using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;
using AdaCredit.UI.UseCases;

namespace AdaCredit.UI.UseCases
{
    public static class EmployeeLogin
    {
        public static bool Login(string username)
        {

            var repository = new EmployeeRepository();
            var employee = repository.CheckUsername(username);
            if (employee == null)
            {
                Console.WriteLine("Usuário não encontrado");
                Console.ReadKey();
                return false;
            }

            Console.WriteLine("Digite a senha atual:");
            string password = Console.ReadLine();

            bool passwordMatch = PasswordHandler.Decrypt(password, employee.Password);
            if (!passwordMatch)
            {
                Console.WriteLine("Senha Inválida");
                Console.ReadKey();
                return false;
            }
            employee.lastLogin = DateTime.Now;
            repository.Save();
            return true;
        }
        
    }
}