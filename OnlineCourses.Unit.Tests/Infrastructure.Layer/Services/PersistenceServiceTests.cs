using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Infrastructure.Layer.Services;

namespace OnlineCourses.Unit.Tests.Infrastructure.Layer.Services {
    public class PersistenceServiceTests {

        private Mock<IWriteRepository<Student>> _writeRepositoryMock;
        private Mock<IAzureQueueSender<Student>> _azureQueueSenderMock;
        private PersistenceService<Student> _service;
        private Student _student;

        [SetUp]
        public void SetUp() {

            _writeRepositoryMock = new Mock<IWriteRepository<Student>>();
            _azureQueueSenderMock = new Mock<IAzureQueueSender<Student>>();
            _service = new PersistenceService<Student>(_writeRepositoryMock.Object, _azureQueueSenderMock.Object);
            _student = CreateStudent();
        }


        [Test]
        public void Save_ShouldCalWriteRepository() {
            _service.Save(_student);

            _writeRepositoryMock.Verify(mock => mock.Save(It.IsAny<Student>()), Times.Once);
            _writeRepositoryMock.Verify(mock => mock.Save(_student));
        }

        [Test]
        public async Task SaveAsync_ShouldCalWriteRepository() {
            await _service.SaveAsync(_student);

            _writeRepositoryMock.Verify(mock => mock.SaveAsync(It.IsAny<Student>()), Times.Once);
            _writeRepositoryMock.Verify(mock => mock.SaveAsync(_student));
        }


        [Test]
        public async Task SendToQueueToSaveAsync_ShouldCallAzureQueueSender() {
            _service.SendToQueueToSaveAsync(_student);

            _azureQueueSenderMock.Verify(mock => mock.SendAsync(It.IsAny<Student>()), Times.Once);
            _azureQueueSenderMock.Verify(mock => mock.SendAsync(_student));
        }

        private Student CreateStudent() {
            return new Student {
                Name = "Name",
                Age = 3,
                Course = new Course {
                    Id = Guid.NewGuid(),
                    Name = "Name"
                }
            };
        }
    }
}
