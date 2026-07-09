using eCommerce.Application.Exceptions;
using eCommerce.Application.Interfaces;
using MediatR;

namespace eCommerce.Application.Features.Options.Commands.DeleteOption
{
    public class DeleteOptionHandler : IRequestHandler<DeleteOptionCommand>
    {
        private readonly IeCommerceContext _context;
        public DeleteOptionHandler(IeCommerceContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteOptionCommand request, CancellationToken ct)
        {
            var option = await _context.Options.FindAsync(request.Id, ct);
            if (option == null) throw new NotFoundException("Option for deletion not found from provided ID");

            _context.Options.Remove(option);
            await _context.SaveChangesAsync(ct);
        }
    }
}
