using Bogus;

namespace AdaCredit.UI.Repositories
{
    public class AccountRepository
    {
        private static List<Account> _accounts = new List<Account>();

        public static Account GetNewUnique()
        {
            var exists = false;
            var accountNumber = "";

            do
            {
                accountNumber = new Faker().Random.ReplaceNumbers("#####-#");
                exists = _accounts.Any(x => x.Number.Equals(accountNumber));
            } while (exists);

            return new Account(accountNumber);
        }
    }
}