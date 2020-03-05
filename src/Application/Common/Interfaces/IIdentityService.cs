using Golobal_IMC_Task.Application.Common.Models;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
