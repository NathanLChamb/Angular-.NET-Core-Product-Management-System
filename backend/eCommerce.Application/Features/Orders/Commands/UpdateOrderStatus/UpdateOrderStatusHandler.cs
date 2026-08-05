using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Orders.Commands.UpdateOrderStatus;
using eCommerce.Application.Features.Orders.DTOs;
using eCommerce.Application.Features.Orders.Mappings;
using eCommerce.Application.Interfaces;
using eCommerce.Domain.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, ReadOrderDto>
{
    private readonly IeCommerceContext _context;

    public UpdateOrderStatusHandler(IeCommerceContext context)
    {
        _context = context;
    }

    public async Task<ReadOrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken ct)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductVariant)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, ct);

        if (order == null) throw new NotFoundException("Order not found.");
        if (order.Status == OrderStatus.Cancelled) throw new BusinessRuleException("Cancelled orders cannot be updated.");
        if (order.Status == OrderStatus.Delivered && request.Status != OrderStatus.Delivered)
            throw new BusinessRuleException("Delivered orders cannot be changed.");

        order.Status = request.Status;

        await _context.SaveChangesAsync(ct);

        return await _context.Orders
            .Where(o => o.Id == order.Id)
            .ToOrderDto()
            .FirstAsync(ct);
    }
}
