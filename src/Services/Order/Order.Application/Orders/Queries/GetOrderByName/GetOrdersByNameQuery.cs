using Order.Application.Dtos;

namespace Order.Application.Orders.Queries.GetOrderByName;

public  record GetOrdersByNameQuery(string  orderName) : IQuery<GetOrderByNameResult>;



public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);