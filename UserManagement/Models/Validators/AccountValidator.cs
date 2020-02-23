using FluentValidation;

namespace UserManagement.Models.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.User.Salary - account.User.Expenses).GreaterThanOrEqualTo(1000);
        }
    }
}