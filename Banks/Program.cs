using System;
using Banks.BankStructure;
using Banks.BankStructure.BankAccounts.AccountCommands;
using Banks.BankStructure.ClientStructure;
using Banks.BankStructure.SavingInterestMechanism;

namespace Banks
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ClientBuilder()
                .BuildName("Hello", "World")
                .BuildAddress("Nice address")
                .BuildPassport("1337").Build();
            var bank = new Bank();
            bank.SetTransferThreshold(10000).SetWithdrawThreshold(5000).SetAddFundsThreshold(4000);
            var interest = new DebitInterest(3.65);
            bank.SetDebitInterest(interest);
            bank.AddClient(client);
            var account = bank.CreateDebitAccount(client);
            var test = new TransactionManager();
            test.SetTransaction(new AddFunds(account, 1337));
            test.Confirm();
            Console.WriteLine(account.Funds);
            bank.BackToTheFuture(2);
            Console.WriteLine(account.Funds);

            var anotherBank = new Bank();
            anotherBank.SetTransferThreshold(10000).SetWithdrawThreshold(5000).SetAddFundsThreshold(4000);
            var anotherClient = new ClientBuilder()
                .BuildName("World", "Hello")
                .BuildAddress("Another nice address")
                .BuildPassport("1404").Build();
            anotherBank.AddClient(anotherClient);
            var anotherAccount = anotherBank.CreateDebitAccount(anotherClient);
            // test.SetTransaction(new Transfer(account, anotherAccount, 600));
            test.SetTransaction(new Transfer(account, anotherBank.BankAccounts[2], 600));
            test.Confirm();
            Console.WriteLine(account.Funds);
            Console.WriteLine(anotherAccount.Funds);
            test.Undo();
            Console.WriteLine(account.Funds);
            Console.WriteLine(anotherAccount.Funds);

        }
    }
}