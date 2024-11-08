using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests;

public class WithdrawRequest
{
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "Invalid account ID")]
    public long AccountId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
}
