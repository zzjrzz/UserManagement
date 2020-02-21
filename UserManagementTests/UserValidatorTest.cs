using FluentValidation.TestHelper;
using Xunit;
using UserManagement.Models.Validators;

namespace UserManagementTests
{
    public class UserValidatorTest
    {
        private readonly UserValidator _validator;

        public UserValidatorTest()
        {
            _validator = new UserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Name, null as string);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Specified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Name, "Jeremy");
        }
    }
}