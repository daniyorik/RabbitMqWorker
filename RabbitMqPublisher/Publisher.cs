using Configurations;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Base;
using RabbitMqEventBus.Events;
using RabbitMqEventBus.Models;
using System;

namespace RabbitMqPublisher
{
    class Publisher
    {
        private static ConfigurationStorage _configuration { get; }
        static Publisher()
        {
            _configuration = new ConfigurationStorage();
        }
        public static void Main()
        {
            var service = _configuration.ServiceProvider.GetRequiredService<INotificationService>();
            var handler = _configuration.ServiceProvider.GetRequiredService<INotificationHandler>();
            Console.WriteLine("Press [Enter] for Send message");
            while(!(Console.KeyAvailable) && Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                var @event = new TestEvent()
                {
                    Id = Guid.NewGuid(),
                    Data = new Data()
                    {
                        Title = "Test Title",
                        Description = "Test Description"
                    }
                };

                handler.EventRaised(@event);
            }
        }
    }
}
