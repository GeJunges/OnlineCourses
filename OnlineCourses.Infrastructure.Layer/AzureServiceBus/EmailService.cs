using OnlineCourses.Domain.Layer.Interfaces;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus {
    public class EmailService : IEmailService {

        private readonly ILoggerWrapper _logger;

        public EmailService(ILoggerWrapper logger) {
            _logger = logger;
        }

        public void SendEmail(string message) {
            _logger.Info(message);
        }
    }
}
