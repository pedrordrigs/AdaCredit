using AdaCredit.UI.Entities;
using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class AddNewEmployee
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o Nome:");
            var name = Console.ReadLine();

            Console.WriteLine("Digite o login:");
            var login = Console.ReadLine();

            Console.WriteLine("Digite o password:");
            var password = Console.ReadLine();

            var employee = new Employee(name, login, password);

            var repository = new EmployeeRepository();
            var result = repository.Add(employee);

            if (result)
                Console.WriteLine("Funcionário cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar novo funcionário!");

            Console.ReadKey();
        }
    }
}
