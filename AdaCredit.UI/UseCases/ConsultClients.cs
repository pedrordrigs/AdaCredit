using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class ConsultClients
    {
        public static void Execute()
        {
            long document;
            while (true)
            {
                Console.WriteLine("Digite o documento do cliente a ser editado:");
                bool parsing = long.TryParse(Console.ReadLine(), out document);
                if (!parsing)
                {
                    Console.WriteLine("Entrada inválida.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                    break;
            }
            var repository = new ClientRepository();
            var client = repository.Find(document);
            if (client == null)
            {
                Console.WriteLine("Cliente não encontrado");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Nome: {client.Name}");
            Console.WriteLine($"Documento: {client.Document}");
            Console.WriteLine($"Conta: {client.Account.Number}");
            Console.WriteLine($"Saldo: {client.Balance}");
            if (client.isActive)
                Console.WriteLine($"Conta ativa");
            else
                Console.WriteLine($"Conta desativada");
            Console.ReadKey();
        }
    }
}