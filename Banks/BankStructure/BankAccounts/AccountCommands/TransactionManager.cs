namespace Banks.BankStructure.BankAccounts.AccountCommands
{
    public class TransactionManager
    {
        private Command _command;

        public void SetTransaction(Command command)
        {
            _command = command;
        }

        public void Confirm()
        {
            _command.Execute();
        }

        public void Undo()
        {
            _command.Undo();
        }
    }
}