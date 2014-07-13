using System;
using FluentValidation;

namespace FluentValidationRemoteClientSideValidation.Models.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        private readonly Func<IApplicationDbContext> dbContextFunction;
        public RegisterViewModelValidator(Func<IApplicationDbContext> dbContextFunction)
        {
            this.dbContextFunction = dbContextFunction;

            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress()
                .SetValidator(new UniqueEmailValidator(dbContextFunction));
            RuleFor(m => m.Password)
                .NotEmpty()
                .Length(6, 100);
            RuleFor(m => m.ConfirmPassword)
                .Equal(m => m.Password);
        }
    }
}