using Golobal_IMC_Task.Application.Categorys.Commands.CreateCategory;
using Golobal_IMC_Task.Application.Categorys.Commands.DeleteCategory;
using Golobal_IMC_Task.Application.Categorys.Commands.UpdateCategory;
using Golobal_IMC_Task.Application.Categorys.Queries.ExportCategorys;
using Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<CategoriesVm>> Get()
        {
            return await Mediator.Send(new GetCategoriesQuery());
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportCategorysQuery { CategoryId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdateCategoryCommand command)
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
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
