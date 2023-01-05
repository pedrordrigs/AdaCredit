using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class ReportInactive
    {
        public static void Execute()
        {
            ClientRepository.GetActive();
            Console.ReadKey();
        }
    }
}