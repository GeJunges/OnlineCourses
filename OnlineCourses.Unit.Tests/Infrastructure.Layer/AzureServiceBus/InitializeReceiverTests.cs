using Moq;
using NUnit.Framework;
using OnlineCourses.Domain.Layer.AzureHelpers;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Infrastructure.Layer.AzureServiceBus;
using System;

namespace OnlineCourses.Unit.Tests.Infrastructure.Layer.AzureServiceBus {
    public class InitializeReceiverTests {

        private InitializeReceiver _initializeReceiver;
        private Mock<ILoggerWrapper> _loggerMock;
        private Mock<IAzureQueueReceiver> _queueReceiverMock;
        private Mock<IPersistenceService<Student>> _serviceMock;
        private Mock<IEmailService> _emailServiceMock;

        [SetUp]
        public void SetUp() {
            _loggerMock = new Mock<ILoggerWrapper>();
            _queueReceiverMock = new Mock<IAzureQueueReceiver>();
            _serviceMock = new Mock<IPersistenceService<Student>>();
            _emailServiceMock = new Mock<IEmailService>();
            _initializeReceiver = new InitializeReceiver(_loggerMock.Object, _queueReceiverMock.Object, _serviceMock.Object, _emailServiceMock.Object);
        }

        [Test]
        public void Init_ShouldCallAzureQueueReceiver() {
            _initializeReceiver.Init();

            _queueReceiverMock.Verify(mock => mock.Receive(It.IsAny<Func<Student, MessageProcessResponse>>(),
                It.IsAny<Action<Exception>>(), It.IsAny<Action>()), Times.Once);
        }
    }
}
