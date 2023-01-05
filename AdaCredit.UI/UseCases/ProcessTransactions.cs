using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class ProcessTransactions
    {
        public static void Execute()
        {
            TransactionRepository.TransactionProcessing();
        }
    }
}