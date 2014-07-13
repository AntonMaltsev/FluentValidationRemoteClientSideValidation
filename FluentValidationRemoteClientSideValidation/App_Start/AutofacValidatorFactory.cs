using System;
using Autofac;
using FluentValidation;

namespace FluentValidationRemoteClientSideValidation
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer container;

        public AutofacValidatorFactory(IContainer container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return container.ResolveOptionalKeyed<IValidator>(validatorType);
        }
    }
}