using System.Text;
using System.Threading;

namespace Banks.BankStructure.ClientStructure
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        public Bank Bank { get; set; }
        
        public bool IsRestricted { get; private set; }

        public void SetRestrictionStatus()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && 
                !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Passport))
            {
                IsRestricted = false;
            }
            else
            {
                IsRestricted = true;
            }
        }
    }
}