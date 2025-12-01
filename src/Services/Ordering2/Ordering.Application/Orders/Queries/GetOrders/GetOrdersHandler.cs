using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.DTO;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext db) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await db.Orders.LongCountAsync(cancellationToken);

        var orders = await db.Orders.Include(o => o.OrderItems)
                     .OrderBy(o => o.OrderName.Value).Skip(pageSize * pageIndex)
                     .Take(pageSize).ToListAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDTO>(pageIndex, pageSize, totalCount, orders.ToOrderDTOList()));
    }
}
