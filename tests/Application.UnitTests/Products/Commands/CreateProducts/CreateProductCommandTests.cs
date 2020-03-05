using Golobal_IMC_Task.Application.Products.Commands.CreateProduct;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Products.Commands.CreateProduct
{
    public class CreateProductCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistProduct()
        {
            var command = new CreateProductCommand
            {
                Title = "Do yet another thing."
            };

            var handler = new CreateProductCommand.CreateProductCommandHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Products.Find(result);

            entity.ShouldNotBeNull();
            entity.Title.ShouldBe(command.Title);
        }
    }
}
