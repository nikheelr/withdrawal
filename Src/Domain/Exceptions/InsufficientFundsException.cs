namespace Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    private const string DefaultErrorMessage = "Insufficient Funds - balance: {0}";

    public InsufficientFundsException(decimal balance)
        : base(string.Format(DefaultErrorMessage, balance))
    {
    }
}