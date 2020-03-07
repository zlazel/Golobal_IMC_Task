using Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.WebUI.Controllers
{
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Login()
        {
            return null; //await Mediator.Send(new GetCategoriesQuery());
        }    
    }
}
