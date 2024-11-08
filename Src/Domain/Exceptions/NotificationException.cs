namespace Domain.Exceptions;

public class NotificationException : Exception
{
    private const string DefaultErrorMessage = "Unable to publish notification error: {0}";
    public NotificationException(string error)
        : base(string.Format(DefaultErrorMessage, error))
    {
    }
}