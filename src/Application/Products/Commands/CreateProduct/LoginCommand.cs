using Golobal_IMC_Task.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Products.Commands.CreateProduct
{
    public class LoginCommand : IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
        {
            private readonly IApplicationDbContext _context;

            public LoginCommandHandler (IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
               

               // _context.Products.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return "";
            }
        }
    }
}
