using Autofac;
using FluentValidationRemoteClientSideValidation.Models;

namespace BetterBuffer.Modules
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                   .As<IApplicationDbContext>()
                   .InstancePerRequest();
        }
    }
}