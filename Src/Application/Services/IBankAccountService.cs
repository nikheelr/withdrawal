namespace Application.Services;

public interface IBankAccountService
{
    Task Withdraw(long accountNumber, decimal amount);
}