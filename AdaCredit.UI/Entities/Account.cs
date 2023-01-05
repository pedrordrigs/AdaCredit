using Bogus;

namespace AdaCredit.UI
{
    public sealed class Account
    {
        public string Number { get; private set; }
        public string Branch { get; private set; }

        public Account()
        {
            Number = new Faker().Random.ReplaceNumbers("#####-#");
            Branch = "0001";
        }

        public Account(string accountNumber)
        {
            Number = accountNumber;
            Branch = "0001";
        }
    }
}
