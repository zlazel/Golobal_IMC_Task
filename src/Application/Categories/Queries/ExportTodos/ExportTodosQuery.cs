using AutoMapper;
using AutoMapper.QueryableExtensions;
using Golobal_IMC_Task.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Queries.ExportCategorys
{
    public class ExportCategorysQuery : IRequest<ExportCategorysVm>
    {
        public int CategoryId { get; set; }

        public class ExportCategorysQueryHandler : IRequestHandler<ExportCategorysQuery, ExportCategorysVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ICsvFileBuilder _fileBuilder;

            public ExportCategorysQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
            {
                _context = context;
                _mapper = mapper;
                _fileBuilder = fileBuilder;
            }

            public async Task<ExportCategorysVm> Handle(ExportCategorysQuery request, CancellationToken cancellationToken)
            {
                var vm = new ExportCategorysVm();

                var records = await _context.Products
                        .Where(t => t.CategoryId == request.CategoryId)
                        .ProjectTo<ProductRecord>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                vm.Content = _fileBuilder.BuildProductsFile(records);
                vm.ContentType = "text/csv";
                vm.FileName = "Products.csv";

                return await Task.FromResult(vm);
            }
        }
    }
}
