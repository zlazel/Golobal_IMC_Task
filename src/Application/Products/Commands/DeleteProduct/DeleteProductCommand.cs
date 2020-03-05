using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Products.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Product), request.Id);
                }

                _context.Products.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
