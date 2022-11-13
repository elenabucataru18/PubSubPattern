namespace PubSubPattern;

public class Program
{
    public static void Main(string[] args)
    {
        var broker = DepartmentBroker.GetInstance();
        broker.StartProcessing();

        var documentToBeCreated = 5;

        var administrator = new Administrator(broker);

        var employees = new List<Employee>{
            new() { Id = 34, Name = "Michael" },
            new() { Id = 33, Name = "Richard" }
        };

        employees[0].StartWork();
        employees[1].StartWork();

        employees[0].Subscribe(broker, DepartmentType.Accounting);
        employees[0].Subscribe(broker, DepartmentType.TaxesFees);
        employees[1].Subscribe(broker, DepartmentType.TaxesFees);

        while (true)
        {
            Thread.Sleep(1000);
            if (documentToBeCreated != 0)
            {
                var documentCreated = administrator.CreateDocument();
                administrator.PublishDocument(documentCreated);

                documentToBeCreated--;
            }
        }
    }
}