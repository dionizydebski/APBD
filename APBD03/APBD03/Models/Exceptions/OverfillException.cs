namespace APBD03.Models.Exceptions;

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message)
    {
    }
}