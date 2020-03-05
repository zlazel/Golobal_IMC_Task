using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categorys.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                entity.CategoryName = request.CategoryName;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
