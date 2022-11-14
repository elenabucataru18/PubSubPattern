namespace PubSubPattern.Services;

public sealed class DepartmentBroker : IBroker
{
    private static DepartmentBroker? _instance;
    private DepartmentBroker() { }

    public static DepartmentBroker GetInstance()
    {
        return _instance ??= new DepartmentBroker();
    }

    public Stack<Document> Documents { get; set; } = new();

    public event IBroker.Notify OnAccountingDepartment;
    public event IBroker.Notify OnAcquisitionDepartment;
    public event IBroker.Notify OnTaxesFeesDepartment;

    public void AddDocument(Document document)
    {
        Documents.Push(document);
    }

    private void Publish(Document document, IBroker.Notify onDepartment)
    {
        if (onDepartment != null)
        {
            var notificationObj = NotificationEventBuilder
                .Create()
                .WithNotificationDate(DateTime.Now)
                .WithDocument(document)
                .Build();

            onDepartment(this, notificationObj);
        }
    }

    public void ProcessSubscribe(DepartmentType departmentType, IBroker.Notify notify)
    {
        switch (departmentType)
        {
            case DepartmentType.Accounting:
                OnAccountingDepartment += notify;
                break;
            case DepartmentType.Acquisition:
                OnAcquisitionDepartment += notify;
                break;
            case DepartmentType.TaxesFees:
                OnTaxesFeesDepartment += notify;
                break;
        }
    }

    public void StartProcessing()
    {
        Console.WriteLine("Broker is working..");
        Task.Run(ProcessDocument);
    }

    private void ProcessDocument()
    {
        while (true)
        {
            Thread.Sleep(50);

            if (Documents.Any())
            {
                var document = Documents.Pop();

                switch (document.DepartmentType)
                {
                    case DepartmentType.Accounting:
                        Publish(document, OnAccountingDepartment);
                        break;
                    case DepartmentType.TaxesFees:
                        Publish(document, OnTaxesFeesDepartment);
                        break;
                    case DepartmentType.Acquisition:
                        Publish(document, OnAcquisitionDepartment);
                        break;
                }
            }
        }
    }
}
