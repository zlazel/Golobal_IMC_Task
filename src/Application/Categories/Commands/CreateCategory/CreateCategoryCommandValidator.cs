using Golobal_IMC_Task.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryName)
                .NotEmpty().WithMessage("CategoryName is required.")
                .MaximumLength(200).WithMessage("CategoryName must not exceed 200 characters.")
                .MustAsync(BeUniquecategoryName).WithMessage("The specified CategoryName already exists.");
        }

        public async Task<bool> BeUniquecategoryName(string categoryName, CancellationToken cancellationToken)
        {
            return await _context.Categorys
                .AllAsync(l => l.CategoryName != categoryName);
        }
    }
}
