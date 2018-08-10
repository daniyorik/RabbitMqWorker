using System;
using Notifications.Services;
using RabbitMqEventBus.Base;

namespace Notifications.Base
{
    public interface INotificationHandler
    {
        event EventHandler<IntegrationEvent> EventHandlerModified;
        void EventRaised(IntegrationEvent @event);
    }
}
