using eCommerce.Application.Features.Orders.Filters;
using eCommerce.Application.Shared;
using MediatR;

namespace eCommerce.Application.Features.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery(
        OrderSearchFilter Filter
    ) : IRequest<PagedResult<ReadOrderFromAdminDto>>;
}
