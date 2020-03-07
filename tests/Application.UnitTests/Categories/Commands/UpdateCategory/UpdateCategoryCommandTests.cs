using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Categorys.Commands.UpdateCategory;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Categorys.Commands.UpdateCategory
{
    public class UpdateCategoryCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldUpdatePersistedCategory()
        {
            var command = new UpdateCategoryCommand
            {
                Id = 1,
                CategoryName = "Shopping",
            };

            var handler = new UpdateCategoryCommand.UpdateCategoryCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Categories.Find(command.Id);

            entity.ShouldNotBeNull();
            entity.CategoryName.ShouldBe(command.CategoryName);
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new UpdateCategoryCommand
            {
                Id = 99,
                CategoryName = "Bucket List",
            };

            var handler = new UpdateCategoryCommand.UpdateCategoryCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
