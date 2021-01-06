namespace Banks.BankStructure.BankAccounts.AccountCommands
{
    public class Transfer : Command
    {
        private BankAccount Sender { get; set; }

        public Transfer(BankAccount sender, BankAccount receiver, decimal processingFunds)
        {
            Sender = sender;
            ProcessingFunds = processingFunds;
            Receiver = receiver;
        }

        public override void Execute()
        {
            if (!Sender.Owner.IsRestricted)
            {
                // Receiver.RestrictedTransfer(Receiver, ProcessingFunds);
                if (Sender.Funds >= ProcessingFunds)
                {
                    Receiver.Funds += ProcessingFunds;
                    Sender.Funds -= ProcessingFunds;
                    Receiver.TransactionHistory.Push(this);
                    Sender.TransactionHistory.Push(this);
                }
                
            }
            
        }

        public override void Undo()
        {
            Receiver.Funds -= Receiver.TransactionHistory.Pop().ProcessingFunds;
            Sender.Funds += Sender.TransactionHistory.Pop().ProcessingFunds;
        }
    }
}