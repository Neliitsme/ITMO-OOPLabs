namespace Banks.BankStructure.BankAccounts.AccountCommands
{
    public abstract class Command
    {
        public BankAccount Receiver { get; set; }
        public decimal ProcessingFunds { get; set; }
        public abstract void Execute();
        public abstract void Undo();
    }
}