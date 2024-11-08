using Application.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly BankContext _context;

    public BankAccountRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<BankAccountEntity?> GetAccount(long accountNumber)
    {
        return await _context.BankAccounts.FirstOrDefaultAsync(acc => acc.AccountNumber == accountNumber);
    }

    public async Task<bool> UpdateBalance(long accountNumber, decimal balance)
    {
        var account = await GetAccount(accountNumber);

        if (account is null) return false;

        account.Balance = balance;

        _context.BankAccounts.Update(account);
        await _context.SaveChangesAsync();
        return true;
    }
}