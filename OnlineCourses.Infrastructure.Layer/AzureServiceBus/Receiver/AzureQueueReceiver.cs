using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineCourses.Domain.Layer.AzureHelpers;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus.Receiver {
    public class AzureQueueReceiver : IAzureQueueReceiver {

        private readonly IConfiguration _configuration;
        private QueueClient _queueClient;
        private ILoggerWrapper _logger;

        public AzureQueueReceiver(IConfiguration configuration, ILoggerWrapper logger) {
            _configuration = configuration;
            _logger = logger;
            Initialize();
        }

        public void Receive(Func<Student, MessageProcessResponse> onProcess,
            Action<Exception> onError,
            Action onWait) {

            var options = new MessageHandlerOptions(e => {
                LogError(e.Exception);
                return Task.CompletedTask;
            }) {
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
            };

            _queueClient.RegisterMessageHandler(
                async (message, token) => {
                    try {
                        var data = Encoding.UTF8.GetString(message.Body);

                        Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{data}");
                        var student = JsonConvert.DeserializeObject<Student>(data);

                        var result = onProcess(student);

                        if (result == MessageProcessResponse.Complete)
                            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                        else if (result == MessageProcessResponse.Abandon)
                            await _queueClient.AbandonAsync(message.SystemProperties.LockToken);
                        else if (result == MessageProcessResponse.Dead)
                            await _queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                        
                        onWait();
                    } catch (Exception ex) {
                        _logger.Error($"Error: {ex.Message}");
                        await _queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                        onError(ex);
                    }
                }, options);
        }

        private void LogError(Exception exception) {
            _logger.Error(exception.Message);
        }

        private void Initialize() {

            var settings = new AzureSettings {
                ConnectionString = _configuration["AzureServiceBus:ConnectionString"],
                QueueName = _configuration["AzureServiceBus:QueueName"],
            };

            _queueClient = new QueueClient(settings.ConnectionString, settings.QueueName);
        }
    }
}
