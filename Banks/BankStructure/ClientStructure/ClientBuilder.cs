namespace Banks.BankStructure.ClientStructure
{
    public class ClientBuilder : IClientBuilder
    {
        private Client _client;

        public ClientBuilder()
        {
            _client = new Client();
        }

        public IClientBuilder BuildName(string firstName, string lastName)
        {
            _client.FirstName = firstName;
            _client.LastName = lastName;
            _client.SetRestrictionStatus();
            return this;
        }

        public IClientBuilder BuildAddress(string address = null)
        {
            _client.Address = address;
            _client.SetRestrictionStatus();
            return this;
        }

        public IClientBuilder BuildPassport(string passport = null)
        {
            _client.Passport = passport;
            _client.SetRestrictionStatus();
            return this;
        }

        public Client Build()
        {
            return _client;
        }
    }
}