using Banks.BankStructure.ClientStructure;

namespace Banks.BankStructure.BankAccounts
{
    public abstract class BankAccountCreator
    {
        public abstract BankAccount FactoryMethod(Client owner);
    }
}