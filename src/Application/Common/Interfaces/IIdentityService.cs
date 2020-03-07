using Golobal_IMC_Task.Application.Common.Models;
using Golobal_IMC_Task.Application.Products.Commands.CreateProduct;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application
{
    public interface IIdentityService
    {
        Task<string> Login(LoginCommand command);
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
