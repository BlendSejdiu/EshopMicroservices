namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventHandler> _logger;

    public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        //if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        //{
        //    var orderCreatedIntegrationEvent = notification.order.ToOrderDTO();
        //    await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        //}
    }
}

