using AutoMapper;
using AutoMapper.QueryableExtensions;
using Golobal_IMC_Task.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys
{
    public class GetCategoriesQuery : IRequest<CategoriesVm>
    {
        public class GetCategorysQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCategorysQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            {
                var vm = new CategoriesVm();

                vm.Categories = await _context.Categorys
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.CategoryName)
                    .ToListAsync(cancellationToken);

                return vm;
            }
        }
    }
}
