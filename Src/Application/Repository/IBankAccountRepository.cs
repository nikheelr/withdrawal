using Domain.Models;

namespace Application.Repository;

public interface IBankAccountRepository
{
    Task<BankAccountEntity?> GetAccount(long accountNumber);

    Task<bool> UpdateBalance(long accountNumber, decimal balance);
}