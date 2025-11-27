namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext db) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        db.Orders.Add(order);
        await db.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }
    private Order CreateNewOrder(OrderDTO orderDTO)
    {
        var shippingAddress = Address.Of(orderDTO.ShippingAddress.FirstName, orderDTO.ShippingAddress.LastName, orderDTO.ShippingAddress.EmailAddress, orderDTO.ShippingAddress.AddressLine, orderDTO.ShippingAddress.Country, orderDTO.ShippingAddress.State, orderDTO.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(orderDTO.BillingAddress.FirstName, orderDTO.BillingAddress.LastName, orderDTO.BillingAddress.EmailAddress, orderDTO.BillingAddress.AddressLine, orderDTO.BillingAddress.Country, orderDTO.BillingAddress.State, orderDTO.BillingAddress.ZipCode);
        var newOrder = Order.Create(
        Id: OrderId.Of(Guid.NewGuid()),
        customerId: CustomerId.Of(orderDTO.CustomerId),
        orderName: OrderName.Of(orderDTO.OrderName),
        shippingAddress: shippingAddress,
        billingAddress: billingAddress,
        payment: Payment.Of(orderDTO.Payment.CardName, orderDTO.Payment.CardNumber, orderDTO.Payment.Expiration, orderDTO.Payment.Cvv, orderDTO.Payment.PaymentMethod)
        );

        foreach (var orderItemDto in orderDTO.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }
        return newOrder;
    }
}