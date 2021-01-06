namespace Banks.BankStructure.BankAccounts.AccountCommands
{
    public class Withdraw : Command
    {
        public Withdraw(BankAccount receiver, decimal processingFunds)
        {
            Receiver = receiver;
            ProcessingFunds = processingFunds;
        }

        public override void Execute()
        {
            if (!Receiver.Owner.IsRestricted)
            {
                if (Receiver.Funds >= ProcessingFunds)
                {
                    Receiver.Funds -= ProcessingFunds;
                    Receiver.TransactionHistory.Push(this);
                }
                // Receiver.RestrictedWithdraw(ProcessingFunds);
            }
            
        }

        public override void Undo()
        {
            Receiver.Funds += Receiver.TransactionHistory.Pop().ProcessingFunds;
        }
    }
}