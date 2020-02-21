using FluentValidation.TestHelper;
using Xunit;
using UserManagement.Models.Validators;

namespace UserManagementTests
{
    public class UserValidatorTest
    {
        [Fact]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            var validator = new UserValidator();
            validator.ShouldHaveValidationErrorFor(user => user.Name, null as string);
        }
    }
}