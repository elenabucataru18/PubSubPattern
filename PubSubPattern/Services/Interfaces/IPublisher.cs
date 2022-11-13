namespace PubSubPattern.Services.Interfaces;

public interface IPublisher
{
    Document CreateDocument();
    void PublishDocument(Document document);
}