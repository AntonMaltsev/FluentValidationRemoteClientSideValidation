using System;
using System.Linq;
using FluentValidation.Validators;

namespace FluentValidationRemoteClientSideValidation.Models.Validators
{
    public class UniqueEmailValidator : PropertyValidator
    {
        private readonly Func<IApplicationDbContext> dbContextFunction;

        public UniqueEmailValidator(Func<IApplicationDbContext> dbContextFunction)
            : base("Email address is already registered")
        {
            this.dbContextFunction = dbContextFunction;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            IApplicationDbContext dbContext = dbContextFunction();
            string emailAddress = context.PropertyValue as string;

            ApplicationUser user = dbContext.Users.FirstOrDefault(u => u.Email == emailAddress);
            return user == null;
        }
    }
}