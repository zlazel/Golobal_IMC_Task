using Golobal_IMC_Task.Application.Categorys.Commands.UpdateCategory;
using Golobal_IMC_Task.Application.UnitTests.Common;
using Golobal_IMC_Task.Domain.Entities;
using Shouldly;
using Xunit;

namespace Golobal_IMC_Task.Application.UnitTests.Categorys.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidatorTests : CommandTestBase
    {
        [Fact]
        public void IsValid_ShouldBeTrue_WhenListTitleIsUnique()
        {
            var command = new UpdateCategoryCommand
            {
                Id = 1,
                CategoryName = "Shopping"
            };

            var validator = new UpdateCategoryCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenListTitleIsNotUnique()
        {
            Context.Categorys.Add(new Category { CategoryName = "Shopping" });
            Context.SaveChanges();

            var command = new UpdateCategoryCommand
            {
                Id = 2,
                CategoryName = "Shopping"
            };

            var validator = new UpdateCategoryCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }
    }
}
