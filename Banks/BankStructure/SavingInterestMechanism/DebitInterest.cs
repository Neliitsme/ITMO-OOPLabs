using System;
using Banks.BankStructure.ClientStructure;

namespace Banks.BankStructure.SavingInterestMechanism
{
    public class DebitInterest : IInterest
    {
        private double Interest { get; set; }
        public decimal ApplyInterest(decimal funds) => funds * (decimal) Interest;
        
        public DebitInterest(double yearlyInterest) => Interest = (yearlyInterest / 365)/100;
    }
}