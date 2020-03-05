using Golobal_IMC_Task.Application.Products.Commands.CreateProduct;
using Shouldly;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Golobal_IMC_Task.WebUI.IntegrationTests.Controllers.Products
{
    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidCreateProductCommand_ReturnsSuccessCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreateProductCommand
            {
                Title = "Do yet another thing."
            };

            var content = IntegrationTestHelper.GetRequestContent(command);

            var response = await client.PostAsync($"/api/Products", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidCreateProductCommand_ReturnsBadRequest()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreateProductCommand
            {
                Title = "This description of this thing will exceed the maximum length. This description of this thing will exceed the maximum length. This description of this thing will exceed the maximum length. This description of this thing will exceed the maximum length."
            };

            var content = IntegrationTestHelper.GetRequestContent(command);

            var response = await client.PostAsync($"/api/Products", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
