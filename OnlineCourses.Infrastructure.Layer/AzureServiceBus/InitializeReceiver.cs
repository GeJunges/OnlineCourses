using Microsoft.Extensions.Configuration;
using OnlineCourses.Domain.Layer.Interfaces;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus {
    public class InitializeReceiver {

        private readonly ILoggerWrapper _logger;
        private readonly IAzureQueueReceiver _queueReceiver;
        private readonly IConfiguration _configuration;

        public InitializeReceiver(ILoggerWrapper logger, IAzureQueueReceiver queueReceiver, IConfiguration configuration) {
            _logger = logger;
            _queueReceiver = queueReceiver;
            _configuration = configuration;
        }
         
    }
}
