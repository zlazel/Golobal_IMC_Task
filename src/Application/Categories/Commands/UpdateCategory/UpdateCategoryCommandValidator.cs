using Golobal_IMC_Task.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Categorys.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryName)
                .NotEmpty().WithMessage("CategoryName is required.")
                .MaximumLength(200).WithMessage("CategoryName must not exceed 200 characters.")
                .MustAsync(BeUniqueCategoryName).WithMessage("The specified CategoryName is already exists.");
        }

        public async Task<bool> BeUniqueCategoryName(UpdateCategoryCommand model, string CategoryName, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.CategoryName != CategoryName);
        }
    }
}
