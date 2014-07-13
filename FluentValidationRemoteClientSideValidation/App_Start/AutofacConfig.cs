using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BetterBuffer.Modules;
using FluentValidation.Mvc;
using FluentValidationRemoteClientSideValidation.Models;
using FluentValidationRemoteClientSideValidation.Models.Validators;
using Owin;

namespace FluentValidationRemoteClientSideValidation
{
    public class AutofacConfig
    {
        public static void RegisterComponents(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register MVC classes
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();
            builder.RegisterModule(new AutofacWebTypesModule());

            // Register Web API classes
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterInstance(HttpRequestScopedFactoryFor<IApplicationDbContext>()); // See comment on method below

            // Register the modules
            builder.RegisterModule<ValidationModule>();
            builder.RegisterModule<PersistenceModule>();

            // Create the container
            var container = builder.Build();

            // Hook up the MVC dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Hook up the Web API dependency resolver
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Hook up Fluent Validation
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new AutofacValidatorFactory(container);
                provider.AddImplicitRequiredValidator = false;

                provider.Add(typeof(UniqueEmailValidator), (metadata, context, description, validator) => new UniqueEmailPropertyValidator(metadata, context, description, validator));
            });
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        // This method is added so that we can get an per-request instance of the DB Context injected in the Validators. 
        public static Func<T> HttpRequestScopedFactoryFor<T>()
        {
            return () => DependencyResolver.Current.GetService<T>();
        }
    }
}