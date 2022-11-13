namespace PubSubPattern.Services;

public class Administrator : IPublisher
{
    private readonly IBroker _broker;
    public Administrator(IBroker broker)
    {
        _broker = broker;
    }

    public Document CreateDocument()
    {
        var random = new Random();
        var registrationNumber = random.Next(1, 200);
        var departmentType = (DepartmentType)random.Next(0, 3);

        var createdDocument = DocumentBuilder.Create()
            .WithRegistrationNumber(registrationNumber)
            .WithDepartmentType(departmentType)
            .Build();

        Console.WriteLine();
        Console.WriteLine($"Created a new document inside {createdDocument.DepartmentType} department.");

        return createdDocument;
    }

    public void PublishDocument(Document document)
    {
        _broker.AddDocument(document);
    }
}