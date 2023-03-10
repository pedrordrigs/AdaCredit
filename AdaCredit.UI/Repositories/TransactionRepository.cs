using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO.Pipes;
using System.Reflection.Metadata;
using System.Xml.Linq;
using AdaCredit.UI.Entities;

namespace AdaCredit.UI.Repositories
{
    public class TransactionRepository
    {
        private static List<Transaction> _transactions = new List<Transaction>();

        public static void TransactionProcessing()
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions");

            // Get a list of all .csv files in the directory
            string[] filePaths = Directory.GetFiles(directoryPath, "*.csv");

            foreach (string filePath in filePaths)
            {
                // Extract the file name and date from the file path
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string[] parts = fileName.Split('-');
                string bankName = string.Join("-", parts.Take(parts.Length - 1));
                DateTime date = DateTime.ParseExact(parts[parts.Length - 1], "yyyyMMdd", CultureInfo.InvariantCulture);

                try
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            int sourceBankCode = int.Parse(values[0]);
                            int sourceBankAgency = int.Parse(values[1]);
                            int sourceBankAccount = int.Parse(values[2]);
                            int destinationBankCode = int.Parse(values[3]);
                            int destinationBankAgency = int.Parse(values[4]);
                            int destinationBankAccount = int.Parse(values[5]);
                            string transactionType = values[6];
                            decimal amount = decimal.Parse(values[7]);
                            var transaction = new Transaction(sourceBankCode, sourceBankAgency, sourceBankAccount,
                                      destinationBankCode, destinationBankAgency, destinationBankAccount,
                                      transactionType, amount);
                            _transactions.Add(transaction);
                        }
                    }
                    var repository = new TransactionRepository();
                    repository.ProcessTransaction(date);
                    repository.TransactionList(date, bankName);
                    _transactions.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
         
        }
    
    public void ProcessTransaction(DateTime date)
        {
            foreach(var transaction in _transactions)
            {
                try
                {
                    decimal tax = 0;
                    DateTime taxDate = new DateTime(2022, 12, 01);
                    var clientDebit = findAccount(transaction.SourceBankAccount);
                    var clientCredit = findAccount(transaction.DestinationBankAccount);
                    var transactionType = transaction.TransactionType;
                    var bankAgency1 = transaction.DestinationBankAgency;
                    var bankCode1 = transaction.DestinationBankCode;
                    var bankAgency2 = transaction.SourceBankAgency;
                    var bankCode2 = transaction.SourceBankCode;

                    if (clientDebit != null && transactionType == "TED" && (date > taxDate))
                    {
                        tax = 5;
                    }

                    else if (clientDebit != null && transactionType == "DOC" && (date > taxDate))
                    {
                        decimal percentage = (transaction.Amount / 100);
                        if (percentage > 5)
                            percentage = 5;
                        tax = 1 + percentage;
                    }

                    if ((bankAgency1 == 0001 && bankCode1 == 777) || (bankAgency2 == 0001 && bankCode2 == 777))
                    {  
                        if (clientDebit != null && clientCredit != null && ((transaction.Amount + tax) < clientDebit.Balance))
                        {
                            var value = transaction.Amount;
                            var debit = value * -1;
                            UpdateBalance(clientDebit, debit);
                            UpdateBalance(clientCredit, value);
                            transaction.Sucess = true;
                        }

                        else if (clientDebit != null && ((transaction.Amount + tax) < clientDebit.Balance))
                        {
                            var value = transaction.Amount;
                            var debit = (value * -1) - tax;
                            UpdateBalance(clientDebit, debit);
                            transaction.Sucess = true;
                        }

                        else if (clientCredit != null)
                        {
                            var value = transaction.Amount;
                            UpdateBalance(clientCredit, value);
                            transaction.Sucess = true;
                        }
                        else
                            transaction.Sucess = false;
                    }

                    else
                        transaction.Sucess = false;
                }
                catch
                {
                    transaction.Sucess = false;
                }
            }
        }
        public static void FailReport()
        {
            _transactions.Clear();
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions", "Failed");
            // Get a list of all .csv files in the directory
            string[] filePaths = Directory.GetFiles(directoryPath, "*.csv");

            foreach (string filePath in filePaths)
            {
                // Extract the file name and date from the file path
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string[] parts = fileName.Split('-');
                string bankName = string.Join("-", parts.Take(parts.Length - 1));

                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        int sourceBankCode = int.Parse(values[0]);
                        int sourceBankAgency = int.Parse(values[1]);
                        int sourceBankAccount = int.Parse(values[2]);
                        int destinationBankCode = int.Parse(values[3]);
                        int destinationBankAgency = int.Parse(values[4]);
                        int destinationBankAccount = int.Parse(values[5]);
                        string transactionType = values[6];
                        decimal amount = decimal.Parse(values[7]);
                        var transaction = new Transaction(sourceBankCode, sourceBankAgency, sourceBankAccount,
                                    destinationBankCode, destinationBankAgency, destinationBankAccount,
                                    transactionType, amount);
                        _transactions.Add(transaction);
                    }
                }

                // Print header
                Console.WriteLine("Relatório de Operações Falhas");
                Console.WriteLine("-------------------------");

                // Print each failed transaction
                foreach (var transaction in _transactions)
                {
                    Console.WriteLine("Arquivo de origem da transação: " + bankName);
                    Console.WriteLine("Código do Banco de Origem: " + transaction.SourceBankCode);
                    Console.WriteLine("Agência do Banco de Origem: " + transaction.SourceBankAgency);
                    Console.WriteLine("Conta do Banco de Origem: " + transaction.SourceBankAccount);
                    Console.WriteLine("Código do Banco de Destino: " + transaction.DestinationBankCode);
                    Console.WriteLine("Agência do Banco de Destino: " + transaction.DestinationBankAgency);
                    Console.WriteLine("Conta do Banco de Destino: " + transaction.DestinationBankAccount);
                    Console.WriteLine("Tipo da Transação: " + transaction.TransactionType);
                    Console.WriteLine("Valor: " + transaction.Amount);
                    Console.WriteLine("-------------------------");
                }
            }
        }
        public void TransactionList(DateTime date, string bankName)
    {
        foreach (var transaction in _transactions)
        {
            string fileDate = date.ToString("yyyyMMdd");
            if (transaction.Sucess)
            {
                // Set the file name and directory path
                string fileName = $"{bankName}-{fileDate}-completed.csv";
                var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions", "Completed");
                directoryPath = Environment.ExpandEnvironmentVariables(directoryPath);

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(directoryPath);

                // Create the full file path
                string filePath = Path.Combine(directoryPath, fileName);

                // Create the .csv file and the StreamWriter object

                        // Write the data to the .csv file
                        File.AppendAllText(filePath, $"{transaction.SourceBankCode}, {transaction.SourceBankAgency}, {transaction.SourceBankAccount}" +
                        $", {transaction.DestinationBankCode}, {transaction.DestinationBankAgency}, {transaction.DestinationBankAccount}" +
                        $",{transaction.TransactionType}, {transaction.Amount}" + Environment.NewLine);
            }
            else
            {
                // Set the file name and directory path
                string fileName = $"{bankName}-{fileDate}-failed.csv";
                var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Transactions", "Failed");
                directoryPath = Environment.ExpandEnvironmentVariables(directoryPath);

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(directoryPath);

                // Create the full file path
                string filePath = Path.Combine(directoryPath, fileName);
                    // Write the data to the .csv file
                    File.AppendAllText(filePath, $"{transaction.SourceBankCode}, {transaction.SourceBankAgency}, {transaction.SourceBankAccount}" +
                    $", {transaction.DestinationBankCode}, {transaction.DestinationBankAgency}, {transaction.DestinationBankAccount}" +
                    $",{transaction.TransactionType}, {transaction.Amount}" + Environment.NewLine);
               
            }
        }
    }

    public Client findAccount(int account) 
        {
            var clients = new ClientRepository();
            var accountExists = clients.FindAccount(account);
            if(accountExists == null) 
            {
                return null;
            }
            return accountExists;
        }
    public void UpdateBalance(Client client, decimal value)
        {
            var clients = new ClientRepository();
            clients.UpdateBalance(client, value);
        }
    }
}

