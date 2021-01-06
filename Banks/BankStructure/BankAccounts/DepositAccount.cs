using Banks.BankStructure.ClientStructure;

namespace Banks.BankStructure.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        public bool isExpired { get; set; } = false;

    }

    public class DepositAccountCreator : BankAccountCreator
    {
        public override BankAccount FactoryMethod(Client owner)
        {
            var account = new DepositAccount() {Owner = owner};
            return account;
        }
    }
}