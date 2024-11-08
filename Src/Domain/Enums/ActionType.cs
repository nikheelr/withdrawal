using System.ComponentModel;

namespace Domain.Enums;

public enum ActionType
{
    [Description("withdrawal")]
    Withdrawal = 1,
    [Description("publishEvent")]
    PublishEvent = 2,
}