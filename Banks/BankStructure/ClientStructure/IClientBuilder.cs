namespace Banks.BankStructure.ClientStructure
{
    public interface IClientBuilder
    {
        IClientBuilder BuildName(string firstName, string lastName);
        IClientBuilder BuildAddress(string address);
        IClientBuilder BuildPassport(string passport);
        Client Build();
    }
}