using AdaCredit.UI.Repositories;

namespace AdaCredit.UI
{
    public sealed class Client
    {
        public string Name { get; private set; }
        public long Document { get; private set; }
        public Account Account { get; private set; } = null;
        public bool isActive { get; private set; }
        public decimal Balance { get; private set; }

        public Client(string name, long document)
        {
            Name = name;
            Document = document;
            Account = null;
            isActive= true;
            Balance = 0;
        }

        public Client(string name, long document, Account account)
        {
            Name = name;
            Document = document;
            Account = account;
            isActive = true;
            Balance = 0;
        }
        public Client(string name, long document, Account account, bool status)
        {
            Name = name;
            Document = document;
            Account = account;
            isActive = status;
            Balance = 0;
        }

        public Client(string name, long document, Account account, bool status, decimal balance)
        {
            Name = name;
            Document = document;
            Account = account;
            isActive = status;
            Balance = balance;
        }
        public void Deactivate()
        {
            isActive = false;
        }

        public void Update(string name, long document, bool status)
        {
            Name = name;
            Document = document;
            isActive = status;
        }

        public void changeBalance(decimal value)
        {
            Balance += value;
        }
    }
}
