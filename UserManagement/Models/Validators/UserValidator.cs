using FluentValidation;

namespace UserManagement.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotNull();
            RuleFor(user => user.Email).NotNull().EmailAddress();
            RuleFor(user => user.Salary).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(user => user.Expenses).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}