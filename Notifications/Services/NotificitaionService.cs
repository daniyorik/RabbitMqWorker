using Notifications.Base;
using RabbitMqEventBus;
using RabbitMqEventBus.Base;
using RabbitMqEventBus.Events;

namespace Notifications.Services
{
    public class NotificationService: INotificationService
    {
        private INotificationPublisher notificationPublisher { get; }

        public NotificationService(INotificationPublisher publisher, INotificationHandler handler)
        {
            notificationPublisher = publisher;
            handler.EventHandlerModified += Publish;
        }

        public void Publish(object sender, IntegrationEvent e)
        {
            notificationPublisher.Publish(e);
        }
    }
}