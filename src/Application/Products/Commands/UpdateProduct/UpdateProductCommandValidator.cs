using FluentValidation;

namespace Golobal_IMC_Task.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
