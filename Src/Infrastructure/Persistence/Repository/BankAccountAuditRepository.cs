using Application.Repository;
using Domain.Models;

namespace Infrastructure.Persistence.Repository;

public class BankAccountAuditRepository : IBankAccountAuditRepository
{
    private readonly BankContext _context;

    public BankAccountAuditRepository(BankContext context)
    {
        _context = context;
    }
    
    public async Task Add(BankAccountAuditLogDTO logEntity)
    {
        var record = new BankAccountAuditLogEntity
        {
            Timestamp = DateTime.UtcNow,
            AccountNumber = logEntity.AccountNumber,
            Details = logEntity.Details,
            Status = logEntity.Status,
            Amount = logEntity.Amount,
            ActionType = logEntity.ActionType
        };

        _context.BankAccountAuditLogs.Add(record);
        await _context.SaveChangesAsync();
    }
}