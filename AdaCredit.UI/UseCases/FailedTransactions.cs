using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class FailedTransactions
    {
        public static void Execute()
        {
            TransactionRepository.FailReport();
            Console.ReadKey();
        }
    }
}