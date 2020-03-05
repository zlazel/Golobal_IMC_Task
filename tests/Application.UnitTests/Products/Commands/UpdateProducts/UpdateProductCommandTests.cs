using Golobal_IMC_Task.Application.Common.Exceptions;
using Golobal_IMC_Task.Application.Products.Commands.UpdateProduct;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldUpdatePersistedProduct()
        {
            var command = new UpdateProductCommand
            {
                Id = 1,
                Title = "This thing is also done.",
            };

            var handler = new UpdateProductCommand.UpdateProductCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Products.Find(command.Id);

            entity.ShouldNotBeNull();
            entity.Title.ShouldBe(command.Title);
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new UpdateProductCommand
            {
                Id = 99,
                Title = "This item doesn't exist.",
            };

            var sut = new UpdateProductCommand.UpdateProductCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() => 
                sut.Handle(command, CancellationToken.None));
        }
    }
}