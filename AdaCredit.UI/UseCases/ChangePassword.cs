using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class ChangePassword
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o login do funcionário que deseja editar a senha:");
            string login = Console.ReadLine();

            var repository = new EmployeeRepository();
            var change = repository.ChangePassword(login);
            Console.Clear();
            if (change)
                Console.WriteLine("Senha alterada com sucesso!");
            else
                Console.WriteLine("Senha inválida.");

            Console.ReadKey();
        }
    }
}
