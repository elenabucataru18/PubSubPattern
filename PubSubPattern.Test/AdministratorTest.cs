namespace PubSubPattern.Test;

public class AdministratorTest
{
    private Mock<IBroker> _brokerMock;
    private Administrator _sut;

    [SetUp]
    public void Setup()
    {
        _brokerMock = new Mock<IBroker>();
        _sut = new Administrator(_brokerMock.Object);
    }

    [Test]
    public void ShouldAddDocument()
    {
        // Act

        var documentCreated = _sut.CreateDocument();
        _sut.PublishDocument(documentCreated);

        // Assert

        _brokerMock.Verify((s) => s.AddDocument(documentCreated), Times.Once);
    }
}