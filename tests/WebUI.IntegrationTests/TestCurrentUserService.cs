using Golobal_IMC_Task.Application.Common.Interfaces;

namespace Golobal_IMC_Task.WebUI.IntegrationTests
{
    public class TestCurrentUserService : ICurrentUserService
    {
        public string UserId => "00000000-0000-0000-0000-000000000000";
    }
}
