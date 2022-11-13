namespace PubSubPattern.Model.Builder;

public class DocumentBuilder
{
    private readonly Document _document;

    private DocumentBuilder()
    {
        _document = new Document();
    }

    public static DocumentBuilder Create() => new();

    public DocumentBuilder WithRegistrationNumber(int registrationNumber)
    {
        _document.RegistrationNumber = registrationNumber;
        return this;
    }

    public DocumentBuilder WithDepartmentType(DepartmentType departmentType)
    {
        _document.DepartmentType = departmentType;
        return this;
    }

    public Document Build() => _document;
}