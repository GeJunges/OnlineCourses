//using Microsoft.Extensions.Configuration;
//using Moq;
//using NUnit.Framework;
//using OnlineCourses.Infrastructure.Layer.AzureServiceBus;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace OnlineCourses.Unit.Tests.Infrastructure.Layer.AzureServiceBus {
//    public class AzureQueueSettingsTests {

//        private ServiceBusConfiguration _settings;
//        private IConfiguration _configuration;

//        [SetUp]
//        public void SetUp() {
//            _configuration = new ConfigurationBuilder()
//                        //.SetBasePath("")
//                        .AddJsonFile("appsettings.json", optional: true)
//                        .AddEnvironmentVariables()
//                        .Build();

//            //_settings = new ServiceBusConfiguration(_configuration);
//        }

//        [Test]
//        public void GetServiceBusConfigurations_Should() {
//           // _settings.GetServiceBusConfigurations();
//        }
//    }
//}
