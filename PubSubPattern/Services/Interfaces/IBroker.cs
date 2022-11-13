namespace PubSubPattern.Services.Interfaces;

public interface IBroker
{
    delegate void Notify(DepartmentBroker department, NotificationEvent notificationEvent);
    void ProcessSubscribe(DepartmentType departmentType, Notify notify);
    void Publish(Document document, Notify onDepartment);
    void StartProcessing();
    void AddDocument(Document document);
}