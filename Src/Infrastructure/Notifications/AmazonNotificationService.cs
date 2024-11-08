using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Application.Configuration;
using Application.Notifications;
using Application.Serialization;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace Infrastructure.Notifications;

public class AmazonNotificationService : INotificationService
{
    private readonly ILogger<AmazonNotificationService> _logger;
    private readonly ISerialization _serialization;
    private readonly NotificationSettings _settings;
    private readonly AmazonSimpleNotificationServiceClient _client;
    private readonly AsyncRetryPolicy _retryPolicy;
    
    public AmazonNotificationService(ILogger<AmazonNotificationService> logger, IOptions<NotificationSettings> notificationSettings, ISerialization serialization)
    {
        _logger = logger;
        _serialization = serialization;
        _settings = notificationSettings.Value;

        _client = new AmazonSimpleNotificationServiceClient(_settings.AWSAccessKeyId, _settings.AWSSecretAccessKey);
        _retryPolicy = Policy
            .Handle<AmazonSimpleNotificationServiceException>(ex => (int)ex.StatusCode >= 500)
            .Or<TimeoutException>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, waitTime, retryCount, context) =>
                {
                    _logger.LogWarning("Retry {RetryCount} after {WaitTime} due to error: {ErrorMessage}", retryCount, waitTime, exception.Message);
                });
    }

    public async Task Publish(object notification)
    {
        try
        {
            var publishRequest = new PublishRequest
            {
                Message = _serialization.SerializeJson(notification),
                TopicArn = _settings.TopicArn
            };
            
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var response = await _client.PublishAsync(publishRequest);
                _logger.LogInformation("Notification published with request id {RequestId} and status code {StatusCode}", 
                    response.MessageId, response.HttpStatusCode);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Error publishing message {message}", ex.Message);
            throw new NotificationException(ex.Message);
        }
    }
}