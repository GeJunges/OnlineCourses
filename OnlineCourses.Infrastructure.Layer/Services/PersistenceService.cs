using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.Services {
    public class PersistenceService<T> : IPersistenceService<T> where T : IEntity {

        private readonly IWriteRepository<T> _writeRepository;
        private readonly IAzureQueueSender<T> _azureQueueSender;

        public PersistenceService(IWriteRepository<T> writeRepository, IAzureQueueSender<T> azureQueueSender) {
            _writeRepository = writeRepository;
            _azureQueueSender = azureQueueSender;
        }

        public void UpdateCourseInformations(Course course) {
            course.CalculateData();
            _writeRepository.Update(course);
        }

        public void Save(T entity) {
            _writeRepository.Save(entity);
        }

        public async Task SaveAsync(T entity) {
            await _writeRepository.SaveAsync(entity);
        }

        public async Task SendToQueueToSaveAsync(T entity) {
            _azureQueueSender.SendAsync(entity);
        }
    }
}
