using Domain.Enums;
using Domain.Extensions;

namespace Domain.Models;

public class BankAccountAuditLogDTO
{
    public long AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string ActionType { get; set; } 
    public string Status { get; set; }    
    public string Details { get; set; }

    public BankAccountAuditLogDTO(long accountNumber, decimal amount, ActionType actionType, Status status, string details)
    {
        AccountNumber = accountNumber;
        Amount = amount;
        ActionType = actionType.GetDescription();
        Status = status.GetDescription();
        Details = details;
    }
}