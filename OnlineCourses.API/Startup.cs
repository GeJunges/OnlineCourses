using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using OnlineCourses.API.Filters;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Infrastructure.Layer.ContextConfiguration;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Infrastructure.Layer.Repositories;
using OnlineCourses.Domain.Layer.Logger;
using OnlineCourses.Infrastructure.Layer.Services;
using OnlineCourses.Infrastructure.Layer.AzureServiceBus.Receiver;
using OnlineCourses.Infrastructure.Layer.AzureServiceBus.Sender;

namespace OnlineCourses.API {
    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        private ILoggerWrapper _logger;

        public void ConfigureServices(IServiceCollection services) {
            services.AddAutoMapper();

            var connectionString = Configuration.GetConnectionString("Development");
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddTransient(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IQueueService<>), typeof(QueueService<>));
            services.AddTransient(typeof(IAzureQueueSender<>), typeof(AzureQueueSender<>));
            services.AddTransient<ILoggerWrapper, LoggerWrapper>();
                        
            services.AddMvc(options => {
                SetFilters(options);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options => {
                SetJsonConfigurations(options);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerWrapper logger) {
            _logger = logger;

           // CallAzureQueueReceiver();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void CallAzureQueueReceiver() {
            var azureQueueReceiver = new AzureQueueReceiver(Configuration, _logger);
            azureQueueReceiver.Receive();
        }

        private static void SetFilters(MvcOptions options) {
            options.Filters.Add(new ExceptionFilter());
            options.Filters.Add(new ValidateModelAttribute());
        }

        private static void SetJsonConfigurations(MvcJsonOptions options) {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
