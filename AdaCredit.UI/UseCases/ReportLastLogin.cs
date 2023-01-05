using AdaCredit.UI.Repositories;

namespace AdaCredit.UI.UseCases
{
    public static class ReportLastLogin
    {
        public static void Execute()
        {
            EmployeeRepository.GetLastLogin();
            Console.ReadKey();
        }
    }
}