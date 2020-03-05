using Golobal_IMC_Task.Application.Products.Commands.UpdateProduct;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.WebUI.IntegrationTests.Controllers.Products
{
    public class Update : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Update(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidUpdateProductCommand_ReturnsSuccessCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new UpdateProductCommand
            { 
                Id = 1, 
                Title = "Do this thing.",
                Done = true
            };

            var content = IntegrationTestHelper.GetRequestContent(command);

            var response = await client.PutAsync($"/api/Products/{command.Id}", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
