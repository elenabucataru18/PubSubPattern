namespace PubSubPattern.Model;

public class NotificationEvent
{

    public Document Document { get; set;  } = null!;
    public DateTime NotificationDate { get; set; }
}