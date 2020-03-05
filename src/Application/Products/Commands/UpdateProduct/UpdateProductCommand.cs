using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Products.Commands.UpdateProduct
{
    public partial class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Products.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Product), request.Id);
                }

                entity.Title = request.Title;
                entity.Price = request.Price;
                entity.CategoryId = request.CategoryId;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
