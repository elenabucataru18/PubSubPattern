namespace PubSubPattern.Test
{
    public class DepartmentBrokerTests
    {
        private Mock<ISubscriber> _subscriberMock1;
        private Mock<ISubscriber> _subscriberMock2;
        private DepartmentBroker _sut;

        [SetUp]
        public void Setup()
        {
            _subscriberMock1 = new Mock<ISubscriber>();
            _subscriberMock2 = new Mock<ISubscriber>();
            _sut = DepartmentBroker.GetInstance();
        }

        [Test]
        public void ShouldBeSingleton()
        {
            // Act
            var broker = DepartmentBroker.GetInstance();

            // Assert
            Assert.AreEqual(_sut.GetHashCode(), broker.GetHashCode());
        }

        [Test]
        public void ShouldSendNotification()
        {
            // Arrange

            _sut.ProcessSubscribe(DepartmentType.Accounting, _subscriberMock1.Object.ProcessDocument);

            // Act

            _sut.StartProcessing();
            _sut.AddDocument(DocumentBuilder
                .Create()
                .WithRegistrationNumber(2)
                .WithDepartmentType(DepartmentType.Accounting)
                .Build());

            Thread.Sleep(200);

            // Assert

            _subscriberMock1.Verify((s) => s.ProcessDocument(It.IsAny<IBroker>(), It.IsAny<NotificationEvent>()), Times.Once);
        }

        [Test]
        public void ShouldNotSendNotification()
        {
            // Arrange
            _sut.ProcessSubscribe(DepartmentType.Acquisition, _subscriberMock1.Object.ProcessDocument);

            // Act

            _sut.StartProcessing();
            _sut.AddDocument(DocumentBuilder
                .Create()
                .WithRegistrationNumber(2)
                .WithDepartmentType(DepartmentType.Accounting)
                .Build());

            Thread.Sleep(200);

            // Assert

            _subscriberMock1.Verify((s) => s.ProcessDocument(It.IsAny<IBroker>(), It.IsAny<NotificationEvent>()), Times.Never);
        }

        [Test]
        public void ShouldSendOncePerSubscriber()
        {
            // Arrange
            _sut.ProcessSubscribe(DepartmentType.Acquisition, _subscriberMock1.Object.ProcessDocument);
            _sut.ProcessSubscribe(DepartmentType.Acquisition, _subscriberMock2.Object.ProcessDocument);

            // Act

            _sut.StartProcessing();
            _sut.AddDocument(DocumentBuilder
                .Create()
                .WithRegistrationNumber(2)
                .WithDepartmentType(DepartmentType.Acquisition)
                .Build());

            Thread.Sleep(200);

            // Assert

            _subscriberMock1.Verify((s) => s.ProcessDocument(It.IsAny<IBroker>(), It.IsAny<NotificationEvent>()), Times.Once);
            _subscriberMock2.Verify((s) => s.ProcessDocument(It.IsAny<IBroker>(), It.IsAny<NotificationEvent>()), Times.Once);
        }

        [Test]
        public void ShouldSendTwoNotifications()
        {
            // Arrange
            _sut.ProcessSubscribe(DepartmentType.Acquisition, _subscriberMock1.Object.ProcessDocument);

            // Act

            _sut.StartProcessing();
            _sut.AddDocument(DocumentBuilder
                .Create()
                .WithRegistrationNumber(2)
                .WithDepartmentType(DepartmentType.Acquisition)
                .Build());

            _sut.AddDocument(DocumentBuilder
                .Create()
                .WithRegistrationNumber(3)
                .WithDepartmentType(DepartmentType.Acquisition)
                .Build());

            Thread.Sleep(200);

            // Assert

            _subscriberMock1.Verify((s) => s.ProcessDocument(It.IsAny<IBroker>(), It.IsAny<NotificationEvent>()), Times.Exactly(2));
        }
    }
}