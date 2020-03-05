using AutoMapper;
using Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Golobal_IMC_Task.Infrastructure.Persistence;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Categorys.Queries.GetCategorys
{
    [Collection("QueryTests")]
    public class GetCategoriesQueryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new GetCategoriesQuery();

            var handler = new GetCategoriesQuery.GetCategorysQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<CategoriesVm>();
            result.Categories.Count.ShouldBe(1);

            var list = result.Categories.First();

            list.Products.Count.ShouldBe(5);
        }
    }
}
