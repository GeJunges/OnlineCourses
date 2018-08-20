using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.AzureServiceBus.Sender {
    public class AzureQueueSender<T> : IAzureQueueSender<T> where T : IEntity {

        private QueueClient _queueClient;
        private readonly IConfiguration _configuration;
        private readonly ILoggerWrapper _logger;

        public AzureQueueSender(IConfiguration configuration, ILoggerWrapper logger) {
            _configuration = configuration;
            _logger = logger;
            Initialize();
        }

        public async Task SendAsync(T item) {
            _logger.Info("Initializing Sending to Queue");
            await SendAsync(item, null);
        }

        public async Task SendAsync(T item, Dictionary<string, object> properties) {
            var json = JsonConvert.SerializeObject(item);
            Console.WriteLine($"Serialized object: {json}");
            var message = new Message(Encoding.UTF8.GetBytes(json));

            if (properties != null) {
                foreach (var prop in properties) {
                    message.UserProperties.Add(prop.Key, prop.Value);
                }
            }

            await _queueClient.SendAsync(message);
            _logger.Info($"Sent to queue: {message}");
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
