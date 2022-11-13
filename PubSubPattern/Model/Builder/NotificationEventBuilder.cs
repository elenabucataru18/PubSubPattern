namespace PubSubPattern.Model.Builder;

public class NotificationEventBuilder
{
    private readonly NotificationEvent _notificationEvent;

    private NotificationEventBuilder()
    {
        _notificationEvent = new NotificationEvent();
    }

    public static NotificationEventBuilder Create() => new();

    public NotificationEventBuilder WithDocument(Document document)
    {
        _notificationEvent.Document = document;
        return this;
    }

    public NotificationEventBuilder WithNotificationDate(DateTime notificationDate)
    {
        _notificationEvent.NotificationDate = notificationDate;
        return this;
    }

    public NotificationEvent Build() => _notificationEvent;
}