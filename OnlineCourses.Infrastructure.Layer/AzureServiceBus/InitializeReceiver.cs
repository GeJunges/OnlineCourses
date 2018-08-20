using OnlineCourses.Domain.Layer.AzureHelpers;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus {
    public class InitializeReceiver : IInitializeReceiver {

        private readonly ILoggerWrapper _logger;
        private readonly IAzureQueueReceiver _receiver;
        private readonly IPersistenceService<Student> _service;
        private readonly IEmailService _emailService;


        public InitializeReceiver(ILoggerWrapper logger, IAzureQueueReceiver receiver,
                                         IPersistenceService<Student> service,
                                         IEmailService emailService) {
            _logger = logger;
            _receiver = receiver;
            _service = service;
            _emailService = emailService;
        }

        public void Init() {

            _receiver.Receive(student => {
                _logger.Info("Saving message");                

                if (!student.Course.HasVacancy) {
                    _emailService.SendEmail($"Mr/Ms {student.Name} your Subscription was denided the course has no more vacancies.");
                    return MessageProcessResponse.Complete;
                }

                 _service.SaveAsync(student).Wait();
                _emailService.SendEmail($"Mr/Ms {student.Name} your Subscription was Successful!");

                var course = student.Course;
                _service.UpdateCourseInformations(course);

                return MessageProcessResponse.Complete;

            }, ex => {
                _logger.Error($"Error: {ex.Message}");
            }, () => { });
        }
    }
}
