using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var user = new User(clientId,dateOfBirth,email,firstName,lastName);

            BuisnessLogic buisnessLogic = new BuisnessLogic(user);
            
            buisnessLogic.SetCreditLimit();

            if (!buisnessLogic.CorrectCreditLimit() || !buisnessLogic.CorrectAge() || !user.CorrectData())
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
