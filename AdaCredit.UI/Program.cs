using AdaCredit.UI.Repositories;

namespace AdaCredit.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var employeeRepository = new EmployeeRepository();

            var loginScreen = new Login(employeeRepository);
            bool isLogged = loginScreen.Show();

            if(isLogged)
            {
                Menu menu = new Menu();
                menu.Show();
            }
            
            
        }
    }
}