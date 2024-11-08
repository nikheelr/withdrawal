namespace Application.Configuration;

public class NotificationSettings
{
    public string AWSAccessKeyId { get; set; }
    public string AWSSecretAccessKey { get; set; }
    public string Region { get; set; }
    public string TopicArn { get; set; }
}