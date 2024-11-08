using Domain.Models;

namespace Application.Repository;

public interface IBankAccountAuditRepository
{
    /// <summary>
    /// Adds a new audit log for the bank account 
    /// </summary>
    /// <param name="logEntity"></param>
    /// <returns></returns>
    Task Add(BankAccountAuditLogDTO logEntity);
}