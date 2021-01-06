using System;
using System.Collections.Generic;
using System.Dynamic;
using Banks.BankStructure.BankAccounts.AccountCommands;
using Banks.BankStructure.ClientStructure;

namespace Banks.BankStructure.BankAccounts
{
    public abstract class BankAccount
    {
        private static int _idGenerator = 1;
        private static int _IdGenerator
        {
            get => _idGenerator++;
            set => _idGenerator = value;
        }

        public int Id { get; set; }
        public Client Owner { get; set; }
        public decimal Funds { get; set; }
        public BankAccount() => Id = _IdGenerator;

        public decimal InterestSavings { get; set; }
        public Stack<Command> TransactionHistory = new Stack<Command>();
        
    }
}