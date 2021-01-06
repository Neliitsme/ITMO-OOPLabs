namespace Banks.BankStructure.SavingInterestMechanism
{
    public interface IInterest
    {
        decimal ApplyInterest(decimal funds);
    }
}