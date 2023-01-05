namespace AdaCredit.UI.Entities
{
    public sealed class Transaction
    {

        // Public properties to access the transaction data
        public int SourceBankCode { get; set; } 
        public int SourceBankAgency { get; set; }
        public int SourceBankAccount { get; set; }
        public int DestinationBankCode { get; set; }
        public int DestinationBankAgency { get; set; }
        public int DestinationBankAccount { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public bool Sucess { get; set; }

        // Constructor with all fields as arguments
        public Transaction(int sourceBankCode, int sourceBankAgency, int sourceBankAccount,
                                  int destinationBankCode, int destinationBankAgency, int destinationBankAccount,
                                  string transactionType, decimal amount)
        {
            SourceBankCode = sourceBankCode;
            SourceBankAgency = sourceBankAgency;
            SourceBankAccount = sourceBankAccount;
            DestinationBankCode = destinationBankCode;
            DestinationBankAgency = destinationBankAgency;
            DestinationBankAccount = destinationBankAccount;
            TransactionType = transactionType;
            Amount = amount;
        }
    }
}

