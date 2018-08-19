using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus.Receiver {
    public class AzureQueueReceiver {

        private readonly IConfiguration _configuration;
        private QueueClient _queueClient;
        private ILoggerWrapper _logger;

        public AzureQueueReceiver(IConfiguration configuration, ILoggerWrapper logger) {
            _configuration = configuration;
            _logger = logger;
            Initialize();
        }

        public void Receive() {

            var options = new MessageHandlerOptions(e => {
                LogError(e.Exception);
                return Task.CompletedTask;
            }) {
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
            };

            _queueClient.RegisterMessageHandler(async (message, token) => {
                try {
                    var data = Encoding.UTF8.GetString(message.Body);
                    var student = JsonConvert.DeserializeObject<Student>(data);

                    _logger.Info($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{data}");

                    await ProcessMessage(student);
                    await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                    _logger.Info("Process Completed");
                } catch (Exception ex) {
                    await _queueClient.DeadLetterAsync(message.SystemProperties.LockToken);
                    LogError(ex);
                }
            }, options);
        }

        private void LogError(Exception exception) {
            _logger.Error(exception.Message);
        }

        private async Task ProcessMessage(Student student) {

            //Inserir AQUI
            SendEmail(student);
        }

        private void SendEmail(Student student) {
            _logger.Info($"Mr/Ms {student.Name} Your Subscription was Successful!");
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
