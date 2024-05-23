namespace RabbitMqProvider.Client.Producer;

public interface IRabbitMqProducer<in T>
{
    void Publish(T @event);
}