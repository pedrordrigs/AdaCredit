using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class DeactivateClient
    {
        public static void Execute()
        {
            long document;
            while (true)
            {
                Console.WriteLine("Digite o documento do cliente a ser desativado:");
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
            var client = repository.Deactivate(document);
            if (client == false)
            {
                Console.WriteLine("Cliente não encontrado");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Cliente desativado");
            Console.ReadKey();
        }
    }
}