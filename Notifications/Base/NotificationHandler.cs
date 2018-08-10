using System;
using RabbitMqEventBus.Base;

namespace Notifications.Base
{
    public class NotificationHandler: INotificationHandler
    {
        public event EventHandler<IntegrationEvent> EventHandlerModified;

        public void EventRaised(IntegrationEvent @event) 
        {
            IntegrationEvent e = @event;
            EventHandler<IntegrationEvent> handler = EventHandlerModified;
            handler?.Invoke(this, e);
        }
    }
}