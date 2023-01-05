namespace AdaCredit.UI.Entities
{
    public sealed class Employee
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }

        public DateTime lastLogin { get; set; }

        public Employee(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
            isActive = true;
            lastLogin = DateTime.Now;
        }
        public Employee(string name, string login, string password, bool status)
        {
            Name = name;
            Login = login;
            Password = password;
            isActive = status;
            lastLogin = DateTime.Now;
        }
        public void Deactivate()
        {
            isActive = false;
        }
    }
}
