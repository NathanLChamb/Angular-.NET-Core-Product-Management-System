using eCommerce.Application.Features.Products.DTOs;
using eCommerce.Application.Shared;
using MediatR;

namespace eCommerce.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery(
        PaginationParams PageParams
        ) : IRequest<PagedResult<ReadProductDto>>;
}
