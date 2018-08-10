using RabbitMqEventBus.Base;
using RabbitMqEventBus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqEventBus.Events
{
    public class TestEvent: IntegrationEvent
    {
        public Data Data { get; set; }
    }
}
