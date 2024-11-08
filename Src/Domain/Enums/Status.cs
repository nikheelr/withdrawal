using System.ComponentModel;

namespace Domain.Enums;

public enum Status
{
    [Description("success")]
    Success = 1,
    [Description("failure")]
    Failure = 2,
}