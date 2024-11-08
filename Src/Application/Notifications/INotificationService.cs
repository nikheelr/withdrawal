namespace Application.Notifications;

public interface INotificationService
{
    Task Publish(object notification);
}