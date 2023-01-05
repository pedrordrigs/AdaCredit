using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class AddNewClient
    {
        public static void Execute()
        {
            Console.WriteLine("Digite o Nome:");
            var name = Console.ReadLine();

            Console.WriteLine("Digite o CPF (sem formatação):");
            var document = long.Parse(Console.ReadLine());

            var client = new Client(name, document);

            var repository = new ClientRepository();
            var result = repository.Add(client);

            if (result)
                Console.WriteLine("Cliente cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar novo cliente!");

            Console.ReadKey();
        }
    }
}
