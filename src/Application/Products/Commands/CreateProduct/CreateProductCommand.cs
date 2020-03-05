using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = new Product
                {
                    CategoryId = request.CategoryId,
                    Title = request.Title,
                    Price = request.Price,
                };

                _context.Products.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
