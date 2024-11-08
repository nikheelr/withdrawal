using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class BankAccountAuditLogEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    
    public long AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string ActionType { get; set; } 
    public string Status { get; set; }    
    public string Details { get; set; }
}