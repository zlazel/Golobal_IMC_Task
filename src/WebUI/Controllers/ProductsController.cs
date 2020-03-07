using Golobal_IMC_Task.Application.Products.Commands.CreateProduct;
using Golobal_IMC_Task.Application.Products.Commands.DeleteProduct;
using Golobal_IMC_Task.Application.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.WebUI.Controllers
{
   //[/ [Authorize]
    public class ProductsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });

            return NoContent();
        }
    }
}
