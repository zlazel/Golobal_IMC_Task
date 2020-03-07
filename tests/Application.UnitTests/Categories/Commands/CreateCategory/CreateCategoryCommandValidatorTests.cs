using Golobal_IMC_Task.Application.Categorys.Commands.CreateCategory;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Golobal_IMC_Task.Domain.Entities;
using Shouldly;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Categorys.Commands.CreateCategory
{
    public class UpdateCategoryCommandValidatorTests : CommandTestBase
    {
        [Fact]
        public void IsValid_ShouldBeTrue_WhenListTitleIsUnique()
        {
            var command = new CreateCategoryCommand
            {
                CategoryName = "Food 2"
            };

            var validator = new CreateCategoryCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenListTitleIsNotUnique()
        {
            Context.Categories.Add(new Category { CategoryName = "Shopping" });
            Context.SaveChanges();

            var command = new CreateCategoryCommand
            {
                CategoryName = "Shopping"
            };

            var validator = new CreateCategoryCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }
    }
}
