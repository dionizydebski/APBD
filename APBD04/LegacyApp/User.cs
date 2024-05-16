using System;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class User
    {
        private IClientRepository _clientRepository;
        public Client Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
        
        public User(IClientRepository clientRepository, int clientId, DateTime dateOfBirth, string emailAddress, string firstName, string lastName)
        {
            _clientRepository = clientRepository;
            Client = _clientRepository.GetById(clientId);;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
        }

        public User(int clientId, DateTime dateOfBirth, string emailAddress, string firstName, string lastName): this(new ClientRepository(), clientId, dateOfBirth, emailAddress, firstName, lastName)
        {
        }

        public bool CorrectData()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                return false;
            }
            
            if (!EmailAddress.Contains("@") && !EmailAddress.Contains("."))
            {
                return false;
            }
            
            return true;
        }
    }
}