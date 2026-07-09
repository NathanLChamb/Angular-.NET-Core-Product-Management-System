using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Features.Options.DTOs;
using eCommerce.Application.Features.Products.DTOs;
using MediatR;

namespace eCommerce.Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(
        int Id
        ) : IRequest<ReadProductDto>;
}
