using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categorys
                    .Where(l => l.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                _context.Categorys.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
