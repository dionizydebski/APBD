using System;
using LegacyApp.Interfaces;

namespace LegacyApp;

public class BuisnessLogic
{
    private IClientRepository _clientRepository;
    private ICreditLimit _getCreditLimit;
    private User _user;
        
    public BuisnessLogic(IClientRepository clientRepository, ICreditLimit creditLimit, User user)
    {
        _clientRepository = clientRepository;
        _getCreditLimit = creditLimit;
        _user = user;
    }
        
    public BuisnessLogic(User user): this(new ClientRepository(), new UserCreditService(), user)
    {
    }
    
    public void SetCreditLimit()
    {
        if (_user.Client.Type == "VeryImportantClient")
        {
            _user.HasCreditLimit = false;
        }
        else if (_user.Client.Type == "ImportantClient")
        {
            int creditLimit = _getCreditLimit.GetCreditLimit(_user.LastName, _user.DateOfBirth);
            creditLimit = creditLimit * 2;
            _user.CreditLimit = creditLimit;
        }
        else
        {
            _user.HasCreditLimit = true;
            int creditLimit = _getCreditLimit.GetCreditLimit(_user.LastName, _user.DateOfBirth);
            _user.CreditLimit = creditLimit;
        }
    }

    public bool CorrectCreditLimit()
    {
        if (_user.HasCreditLimit && _user.CreditLimit < 500)
        {
            return false;
        }
        return true;
    }
    
    public bool CorrectAge()
    {
        var now = DateTime.Now;
        int age = now.Year - _user.DateOfBirth.Year;
        if (now.Month < _user.DateOfBirth.Month || (now.Month == _user.DateOfBirth.Month && now.Day < _user.DateOfBirth.Day)) age--;

        if (age < 21)
        {
            return false;
        }
        return true;
    }
}