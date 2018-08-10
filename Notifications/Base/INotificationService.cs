using RabbitMqEventBus.Base;

namespace Notifications.Base
{
    public interface INotificationService 
    {
        void Publish(object sender, IntegrationEvent e);
    }
}