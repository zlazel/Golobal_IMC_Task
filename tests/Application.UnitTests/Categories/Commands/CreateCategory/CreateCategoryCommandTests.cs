using Golobal_IMC_Task.Application.Categorys.Commands.CreateCategory;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Categorys.Commands.CreateCategory
{
    public class CreateCategoryCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistCategory()
        {
            var command = new CreateCategoryCommand
            {
                CategoryName = "Food"
            };

            var handler = new CreateCategoryCommand.CreateCategoryCommandHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Categorys.Find(result);

            entity.ShouldNotBeNull();
            entity.CategoryName.ShouldBe(command.CategoryName);
        }
    }
}
