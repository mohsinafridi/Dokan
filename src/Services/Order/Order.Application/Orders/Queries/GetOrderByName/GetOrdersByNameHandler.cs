using Microsoft.EntityFrameworkCore;


namespace Order.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(ILogger<GetOrdersByNameHandler> logger , IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("asd");

        var orders = await dbContext.Orders
            .Include(o=>o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.orderName))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);
       
        return new GetOrderByNameResult(orders.ToOrderDtoList()); 
    }

   
}
