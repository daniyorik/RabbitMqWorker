using Newtonsoft.Json;
using RabbitMqEventBus.Base;
using RabbitMqEventBus.Events;
using RabbitMqEventBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RabbitMqEventBus.Handlers
{
    public class TestEventHandler : IIntegrationEventHandler<TestEvent>
    {
        public async Task Handle(TestEvent @event)
        {
            Console.WriteLine("1: " + @event.Id);
            Console.WriteLine("2: " + @event.Data.Title);
            Console.WriteLine("3: " + @event.Data.Description);
        }
    }
}
