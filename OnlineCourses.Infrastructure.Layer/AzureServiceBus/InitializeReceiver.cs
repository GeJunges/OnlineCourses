using OnlineCourses.Domain.Layer.AzureHelpers;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus {
    public class InitializeReceiver : IInitializeReceiver {

        private readonly ILoggerWrapper _logger;
        private readonly IAzureQueueReceiver _receiver;
        private readonly IPersistenceService<Student> _service;

        public InitializeReceiver(ILoggerWrapper logger, IAzureQueueReceiver receiver, IPersistenceService<Student> service) {
            _logger = logger;
            _receiver = receiver;
            _service = service;
        }

        public void Init() {

            _receiver.Receive(student => {
                _logger.Info("Saving data");
                _service.SaveAsync(student);
                SendEmail(student);

                return MessageProcessResponse.Complete;

            }, ex => {
                _logger.Error($"Error: {ex.Message}");
            }, () => { });
        }

        private void SendEmail(Student student) {
            _logger.Info($"Mr/Ms {student.Name} Your Subscription was Successful!");
        }
    }
}
