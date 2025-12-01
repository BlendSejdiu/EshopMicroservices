namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(IApplicationDbContext db) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await db.Orders.Include(o => o.OrderItems).AsNoTracking()
                              .Where(o => o.OrderName.Value.Contains(query.Name))
                              .OrderBy(o => o.OrderName).ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToOrderDTOList());
    }
}
