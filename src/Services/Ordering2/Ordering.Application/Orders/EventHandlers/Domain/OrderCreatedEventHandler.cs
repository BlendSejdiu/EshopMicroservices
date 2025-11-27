namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        //if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        //{
        //    var orderCreatedIntegrationEvent = notification.order.ToOrderDTO();
        //    await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        //}
    }
}

