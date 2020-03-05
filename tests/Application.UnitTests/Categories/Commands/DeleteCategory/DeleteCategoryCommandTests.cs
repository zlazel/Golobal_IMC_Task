using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Products.Commands.DeleteProduct;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Categorys.Commands.DeleteCategory
{
    public class DeleteCategoryCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldRemovePersistedProduct()
        {
            var command = new DeleteProductCommand
            {
                Id = 1
            };

            var handler = new DeleteProductCommand.DeleteProductCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Products.Find(command.Id);

            entity.ShouldBeNull();
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteProductCommand
            {
                Id = 99
            };

            var handler = new DeleteProductCommand.DeleteProductCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
