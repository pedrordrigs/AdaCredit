using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class ReportActive
    {
        public static void Execute()
        {
            ClientRepository.GetActive();
            Console.ReadKey();
        }
    }
}