using Golobal_IMC_Task.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Golobal_IMC_Task.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
