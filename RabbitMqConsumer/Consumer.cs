
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using RabbitMqEventBus;
using RabbitMqEventBus.Base;
using Microsoft.Extensions.Configuration;
using RabbitMqEventBus.Events;
using RabbitMqEventBus.Handlers;
using Configurations;

namespace RabbitMqConsumer
{

    class Consumer
    {
        private static ConfigurationStorage configurationStorage { get; set; }
        static Consumer()
        {
            configurationStorage = new ConfigurationStorage();
        }
        public static void Main()
        {
            var eventBus = configurationStorage.ServiceProvider.GetRequiredService<INotificationPublisher>();
            eventBus.Subscribe<TestEvent, TestEventHandler>();
        }
    }
}