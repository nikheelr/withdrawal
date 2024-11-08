namespace Domain.Events;

public class WithdrawalEvent
{
    public decimal Amount { get; set; }
    public long AccountId { get; set; }
    public string Status { get; set; }

    public WithdrawalEvent(decimal amount, long accountId, string status)
    {
        Amount = amount;
        AccountId = accountId;
        Status = status;
    }

    public string ToJson()
    {
        return string.Empty;
    }
}