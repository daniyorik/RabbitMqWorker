using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Base;
using Notifications.Services;
using RabbitMQ.Client;
using RabbitMqEventBus;
using RabbitMqEventBus.Base;
using RabbitMqEventBus.Handlers;
using System;
using System.IO;

namespace Configurations {

    public class ConfigurationStorage
    {
        public string ConnectionString { get; private set; }

        public IServiceProvider ServiceProvider { get; set; }

        public ConfigurationStorage()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            ServiceProvider = new ServiceCollection()
                .AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionManager>()
                .AddSingleton<IRabbitMqPersistentConnection, DefaultRabbitMqPersistentConnection>(sp =>
                {
                    var rabbitmqsection = configuration.GetSection("RabbitMqConnection");
                    int port;
                    Int32.TryParse(rabbitmqsection.GetSection("Port").Value, out port);
                    var factory = new ConnectionFactory
                    {
                        UserName = rabbitmqsection.GetSection("UserName").Value,
                        Password = rabbitmqsection.GetSection("Password").Value,
                        Port = port,
                        VirtualHost = rabbitmqsection.GetSection("VHost").Value,
                        HostName = rabbitmqsection.GetSection("HostName").Value
                    };
                    //we can set retry count into appsettings. Now defaults 5. 
                    return new DefaultRabbitMqPersistentConnection(factory);
                })
                .AddSingleton<INotificationPublisher, NotificationPublisher>(sp => {
                    var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                    var subsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var provider = sp.GetRequiredService<IServiceProvider>();
                    return new NotificationPublisher(rabbitMqPersistentConnection, subsManager, provider);
                })
                .AddSingleton<INotificationHandler, NotificationHandler>()
                .AddSingleton<INotificationService, NotificationService>()
                .AddTransient<TestEventHandler>()
                .BuildServiceProvider();

        }
    }
}
