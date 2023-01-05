using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class DeactivateEmployee
    {
        public static void Execute()
        {

            Console.WriteLine("Digite o login do funcionário a ser desativado:");
            var user = Console.ReadLine();
            var repository = new EmployeeRepository();
            var employee = repository.Deactivate(user);
            if (employee == false)
            {
                Console.WriteLine("Login não encontrado");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Funcionário desativado");
            Console.ReadKey();
        }
    }
}