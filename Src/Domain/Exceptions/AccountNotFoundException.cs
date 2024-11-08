namespace Domain.Exceptions;

public class AccountNotFoundException : Exception
{
    private const string DefaultErrorMessage = "Account with account number {0} was not found.";
    public AccountNotFoundException(long accountNumber)
        : base(string.Format(DefaultErrorMessage, accountNumber))
    {
    }
}