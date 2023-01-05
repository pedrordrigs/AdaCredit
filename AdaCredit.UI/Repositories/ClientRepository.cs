using System;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Xml.Linq;

namespace AdaCredit.UI.Repositories
{
    public class ClientRepository
    {
        private static List<Client> _clients = new List<Client>();
        private static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Clients", "Clients.csv");

        static ClientRepository()
        {
            try
            {
                // Read the file and store the contents in the _clients list
                using (var reader = new StreamReader(_filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var name = values[0];
                        long document = long.Parse(values[1]);
                        var account = new Account(values[2]);
                        bool status = bool.Parse(values[3]);
                        decimal balance = decimal.Parse(values[4]);
                        var client = new Client(name, document, account, status, balance);
                        _clients.Add(client);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool Add(Client client)
        {
            if (_clients.Any(x => x.Document.Equals(client.Document)))
            {
                Console.WriteLine("Cliente já cadastrado");
                Console.ReadKey();

                return false;
            }

            var entity = new Client(client.Name, client.Document, AccountRepository.GetNewUnique(), client.isActive);
            _clients.Add(entity);

            Save();

            return true;
        }

        public bool Update(long document)
        {
            var repository = new ClientRepository();
            var client = _clients.SingleOrDefault(x => x.Document.Equals(document));
            if (client == null)
            {
                Console.WriteLine("Cliente não encontrado");
                return false;
            }
            try
            {
                Console.WriteLine("Digite o Nome:");
                var name = Console.ReadLine();

                Console.WriteLine("Digite o CPF (sem formatação):");
                var new_document = long.Parse(Console.ReadLine());

                Console.WriteLine("Digite o status da conta (true = ativa, false = desativada):");
                var status = bool.Parse(Console.ReadLine());

                client.Update(name, new_document, status);

                Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("Informações inválidas.");
                return false;
            }
            return true;
        }

        public Client Find(long document)
        {
            var repository = new ClientRepository();
            var client = _clients.SingleOrDefault(x => x.Document.Equals(document));
            if (client == null)
            {
                return null;
            }
            else
                return client;
        }

        public Client FindAccount(int account)
        {
            var repository = new ClientRepository();
            string str = account.ToString();
            str = str.Insert(str.Length - 1, "-");
            var client = _clients.SingleOrDefault(x => x.Account.Number.Equals(str));
            if (client != null)
            {
                return client;
            }
            return null;
        }

        public static void GetActive()
        {
            foreach (var client in _clients)
            {
                if (client.isActive)
                {
                    Console.WriteLine($"Nome: {client.Name}");
                    Console.WriteLine($"CPF: {client.Document}");
                    Console.WriteLine($"Conta:{client.Account.Number}");
                    Console.WriteLine($"Saldo:{client.Balance}");
                    Console.WriteLine("");
                }
            }
        }

        public static void GetInactive()
        {
            foreach (var client in _clients)
            {
                if (!client.isActive)
                {
                    Console.WriteLine($"Nome: {client.Name}");
                    Console.WriteLine($"CPF: {client.Document}");
                    Console.WriteLine($"Conta:{client.Account.Number}");
                    Console.WriteLine($"Saldo:{client.Balance}");
                    Console.WriteLine("");
                }
            }
        }
        public bool Deactivate(long document)
        {
            var repository = new ClientRepository();
            var client = _clients.SingleOrDefault(x => x.Document.Equals(document));
            if (client == null)
            {
                return false;
            }
            client.Deactivate();
            repository.Save();
            return true;  
        }

        public void UpdateBalance(Client client, decimal value)
        {
            client.changeBalance(value);
            Save();
        }

        public void Save()
        {
            using (var writer = new StreamWriter(_filePath))
            {
                foreach (var client in _clients)
                {
                    writer.WriteLine($"{client.Name},{client.Document},{client.Account.Number},{client.isActive},{client.Balance}");
                }
            }
        }
    }
}
