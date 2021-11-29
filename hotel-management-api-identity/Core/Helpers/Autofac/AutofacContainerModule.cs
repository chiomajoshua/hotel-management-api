using Autofac;
using hotel_management_api_identity.Core.Storage.QueryRepository;

namespace hotel_management_api_identity.Core.Helpers
{
    public class AutofacContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DapperCommand<>))
             .As(typeof(IDapperCommand<>))
             .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(DapperQuery<>))
             .As(typeof(IDapperQuery<>))
             .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IAutoDependencyCore).Assembly)
             .AssignableTo<IAutoDependencyCore>()
             .As<IAutoDependencyCore>()
             .AsImplementedInterfaces().InstancePerLifetimeScope();

           base.Load(builder);
        }
    }
}