using System;

namespace LegacyApp.Interfaces;

public interface ICreditLimit
{
    int GetCreditLimit(string lastName, DateTime birthdate);
}