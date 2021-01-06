using System;
using Banks.BankStructure.BankAccounts.AccountCommands;
using Banks.BankStructure.ClientStructure;

namespace Banks.BankStructure.BankAccounts
{
    public class CreditAccount : BankAccount
    {
       public decimal MaxDebt { get; set; }
       
    }

    public class CreditAccountCreator : BankAccountCreator
    {
        public override BankAccount FactoryMethod(Client owner)
        {
            var account = new CreditAccount() {Owner = owner};
            return account;
        }
    }
}