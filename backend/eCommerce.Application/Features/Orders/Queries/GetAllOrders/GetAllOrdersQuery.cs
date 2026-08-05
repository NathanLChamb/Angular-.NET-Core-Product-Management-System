using MediatR;

namespace eCommerce.Application.Features.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery()
        : IRequest<List<ReadOrderFromAdminDto>>;
}
