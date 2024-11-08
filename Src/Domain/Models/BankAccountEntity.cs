using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class BankAccountEntity
{
    [Key] 
    public long AccountNumber { get; set; }

    public string Name { get; set; }

    public decimal Balance { get; set; }
}