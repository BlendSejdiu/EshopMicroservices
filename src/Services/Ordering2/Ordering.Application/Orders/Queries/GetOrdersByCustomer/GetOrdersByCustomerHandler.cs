using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext db) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await db.Orders.Include(o => o.OrderItems)
                        .AsNoTracking()
                        .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                        .OrderBy(o => o.OrderName.Value)
                        .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToOrderDTOList());
    }
}
