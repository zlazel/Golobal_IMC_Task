using Golobal_IMC_Task.Application.Common.Behaviours;
using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Application.Products.Commands.CreateProduct;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Common.Behaviours
{
    public class BehaviourTests
    {
        private const string UserId = "jasont";
        private const string UserName = "jason.taylor";

        public BehaviourTests()
        {
        }

        [Fact]
        public void RequestLogger_Should_Call_GetUserNameAsync_Once_If_Authenticated()
        {
            var logger = new Mock<ILogger<CreateProductCommand>>();
            var currentUserService = new Mock<ICurrentUserService>();
            var identityService = new Mock<IIdentityService>();

            currentUserService.Setup(x => x.UserId).Returns(UserId);

            IRequestPreProcessor<CreateProductCommand> requestLogger = new RequestLogger<CreateProductCommand>(logger.Object, currentUserService.Object, identityService.Object);

            requestLogger.Process(new CreateProductCommand { CategoryId = 1, Title = "title" }, new CancellationToken());

            identityService.Verify(i => i.GetUserNameAsync(UserId), Times.Once);
        }

        [Fact]
        public void RequestLogger_Should_Not_Call_GetUserNameAsync_Once_If_Unauthenticated()
        {
            var logger = new Mock<ILogger<CreateProductCommand>>();
            var currentUserService = new Mock<ICurrentUserService>();
            var identityService = new Mock<IIdentityService>();

            currentUserService.Setup(x => x.UserId).Returns((string)null);

            IRequestPreProcessor<CreateProductCommand> requestLogger = new RequestLogger<CreateProductCommand>(logger.Object, currentUserService.Object, identityService.Object);

            requestLogger.Process(new CreateProductCommand { CategoryId = 1, Title = "title" }, new CancellationToken());

            identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}
