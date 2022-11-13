namespace PubSubPattern.Services.Interfaces;

public interface ISubscriber
{
    void Subscribe(IBroker broker, DepartmentType departmentType);
    void ProcessDocument(IBroker broker, NotificationEvent notificationEvent);
    void StartWork();
}