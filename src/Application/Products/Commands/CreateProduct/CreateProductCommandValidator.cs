using FluentValidation;

namespace Golobal_IMC_Task.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
