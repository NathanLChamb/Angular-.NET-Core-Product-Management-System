using eCommerce.Application.Features.Orders.DTOs;
using MediatR;

namespace eCommerce.Application.Features.Orders.Queries.GetMyOrders
{
    public record GetMyOrdersQuery(
        string UserId
    ) : IRequest<List<ReadOrderDto>>;
}
