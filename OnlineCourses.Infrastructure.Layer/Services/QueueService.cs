using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.Services {
    public class QueueService<T> : IQueueService<T> where T : IEntity {

        private readonly IWriteRepository<T> _writeRepository;
        private readonly IAzureQueueSender<T> _azureQueueSender;

        public QueueService(IWriteRepository<T> writeRepository, IAzureQueueSender<T> azureQueueSender) {
            _writeRepository = writeRepository;
            _azureQueueSender = azureQueueSender;
        }

        public void Save(T entity) {
            _writeRepository.Save(entity);
        }

        public async Task SaveAsync(T entity) {
            _azureQueueSender.SendAsync(entity);
        }
    }
}
