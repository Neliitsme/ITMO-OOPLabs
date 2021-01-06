using System.Dynamic;

namespace Banks.BankStructure.BankAccounts.AccountCommands
{
    public class AddFunds : Command
    {
        public AddFunds(BankAccount receiver, decimal moneyToBeAdded)
        {
            Receiver = receiver;
            ProcessingFunds = moneyToBeAdded;
        }

        public override void Execute()
        {
            if (!Receiver.Owner.IsRestricted)
            {
                // Receiver.RestrictedAddFunds(ProcessingFunds);
                Receiver.Funds += ProcessingFunds;
                Receiver.TransactionHistory.Push(this);
            }
            
        }

        public override void Undo()
        {
            Receiver.Funds -= Receiver.TransactionHistory.Pop().ProcessingFunds;
        }
    }
}