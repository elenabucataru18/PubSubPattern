namespace PubSubPattern.Services;

internal class Employee : ISubscriber
{
    public int Id { get; set; }
    public string Name { get; set; }

    private List<Document> _resolvedDocuments = new();

    private Queue<Document> _queueDocuments = new();

    public void Subscribe(IBroker department, DepartmentType departmentEnum)
    {
        department.ProcessSubscribe(departmentEnum, ProcessDocument);
    }

    public virtual void ProcessDocument(IBroker broker, NotificationEvent notificationEvent)
    {
        Console.WriteLine($"Hello, {0}! A new document with id: {1} has been added in your {2} department.",
            Name, notificationEvent.Document.RegistrationNumber, notificationEvent.Document.DepartmentType);

        _queueDocuments.Enqueue(notificationEvent.Document);
    }

    public void StartWork()
    {
        _queueDocuments = new Queue<Document>();

        Console.WriteLine($"{Name} is ready to review documents..");

        Task.Run(Work);
    }

    private void Work()
    {
        while (true)
        {
            Thread.Sleep(50);
            if (_queueDocuments.Any())
            {
                var document = _queueDocuments.Dequeue();
                _resolvedDocuments.Add(document);

                Console.WriteLine($"Employee {0} reviewed document with {1} number. Number of reviewed documents {2}.",
                    Name, document.RegistrationNumber, _resolvedDocuments.Count);
            }
        }
    }


}
