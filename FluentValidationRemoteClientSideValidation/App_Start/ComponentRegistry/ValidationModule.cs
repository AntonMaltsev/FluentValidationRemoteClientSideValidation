using Autofac;
using FluentValidation;
using FluentValidationRemoteClientSideValidation.Models;
using FluentValidationRemoteClientSideValidation.Models.Validators;

namespace FluentValidationRemoteClientSideValidation
{
    public class ValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterViewModelValidator>()
                   .Keyed<IValidator>(typeof (IValidator<RegisterViewModel>))
                   .As<IValidator>();
        }
    }
}