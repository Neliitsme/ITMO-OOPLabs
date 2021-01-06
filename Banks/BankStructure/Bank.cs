using System.Collections.Generic;
using Banks.BankStructure.BankAccounts;
using Banks.BankStructure.ClientStructure;
using Banks.BankStructure.SavingInterestMechanism;

namespace Banks.BankStructure
{
    public class Bank
    {
        // public static Dictionary<int, Bank> BaseOfAllAccounts = new Dictionary<int, Bank>();
        public Dictionary<Client, List<BankAccounts.BankAccount>> Clients = new Dictionary<Client, List<BankAccounts.BankAccount>>();
        public Dictionary<int, BankAccount> BankAccounts = new Dictionary<int, BankAccount>();
        private IInterest _debitInterest;
        private IInterest _depositInterest;
        private IInterest _creditInterest;
        //Todo: Implement restrictions
        public decimal WithdrawThreshold { get; set; } // For restricted accounts
        public decimal TransferThreshold { get; set; } // For restricted accounts
        public decimal AddFundsThreshold { get; set; } // For restricted accounts

        public Bank SetWithdrawThreshold(decimal amount)
        {
            WithdrawThreshold = amount;
            return this;
        }
        
        public Bank SetTransferThreshold(decimal amount)
        {
            TransferThreshold = amount;
            return this;
        }
        
        public Bank SetAddFundsThreshold(decimal amount)
        {
            AddFundsThreshold = amount;
            return this;
        }
        
        public void BackToTheFuture(int months)
        {

            for (int i = 0; i < months; i++)
            {
                foreach (var client in Clients)
                {
                    foreach (var bankAccount in client.Value)
                    {
                        if (bankAccount is DebitAccount)
                        {
                            bankAccount.InterestSavings += _debitInterest.ApplyInterest(bankAccount.Funds);   
                        }
                        else if (bankAccount is DepositAccount)
                        {
                            bankAccount.InterestSavings += _depositInterest.ApplyInterest(bankAccount.Funds);
                        }
                        else if (bankAccount is CreditAccount)
                        {
                            bankAccount.InterestSavings -= _creditInterest.ApplyInterest(bankAccount.Funds);
                        }
                    }
                }
            }

            foreach (var client in Clients)
            {
                foreach (var bankAccount in client.Value)
                {
                    bankAccount.Funds += bankAccount.InterestSavings;
                    bankAccount.InterestSavings = 0;
                }
            }
            
        }
        
        public Bank SetDebitInterest(IInterest interest)
        {
            _debitInterest = interest;
            return this;
        }

        public Bank SetDepositInterest(IInterest interest)
        {
            _depositInterest = interest;
            return this;
        }

        public Bank SetCreditInterest(IInterest interest)
        {
            _creditInterest = interest;
            return this;
        }

        public void AddClient(Client client)
        {
            Clients.Add(client, new List<BankAccount>());
        }

        public BankAccount CreateCreditAccount(Client client)
        {
            var account = new CreditAccountCreator().FactoryMethod(client);
            Clients[client].Add(account);
            BankAccounts.Add(account.Id, account);
            return account;
        }

        public BankAccount CreateDebitAccount(Client client)
        {
            var account = new DebitAccountCreator().FactoryMethod(client);
            Clients[client].Add(account);
            BankAccounts.Add(account.Id, account);
            return account;
        }

        public BankAccount CreateDepositAccount(Client client)
        {
            var account = new DepositAccountCreator().FactoryMethod(client);
            Clients[client].Add(account);
            BankAccounts.Add(account.Id, account);
            return account;
        }
    }
}