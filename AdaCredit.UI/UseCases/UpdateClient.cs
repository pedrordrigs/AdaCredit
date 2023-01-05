using AdaCredit.UI.Repositories;
using System.Data;

namespace AdaCredit.UI.UseCases
{
    public static class UpdateClient
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
            var result = repository.Update(document);

            if (result)
                Console.WriteLine("Cliente editado com sucesso!");
            else
                Console.WriteLine("Falha ao editar cliente!");

            Console.ReadKey();
        }
    }
}
