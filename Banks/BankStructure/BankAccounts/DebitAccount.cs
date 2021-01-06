using System;
using Banks.BankStructure.ClientStructure;

namespace Banks.BankStructure.BankAccounts
{
    public class DebitAccount : BankAccount
    {
        //Todo: Add logic
       
    }

    public class DebitAccountCreator : BankAccountCreator
    {
        public override BankAccount FactoryMethod(Client owner)
        {
            var account = new DebitAccount {Owner = owner};
            return account;
        }
    }
}