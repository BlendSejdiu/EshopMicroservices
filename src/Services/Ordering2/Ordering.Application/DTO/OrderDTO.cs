namespace Ordering.Application.DTO;

public record OrderDTO
    (
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDTO ShippingAddress,
    AddressDTO BillingAddress,
    PaymentDTO Payment,
    OrderStatus Status,
    List<OrderItemDTO> OrderItems
    );