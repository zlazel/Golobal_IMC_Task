using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Commands.CreateCategory
{
    public partial class CreateCategoryCommand : IRequest<int>
    {
        public string CategoryName { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category();

                entity.CategoryName = request.CategoryName;

                _context.Categorys.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
