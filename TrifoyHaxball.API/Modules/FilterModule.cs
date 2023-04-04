using Autofac;
using TrifoyHaxball.API.Filters;

namespace TrifoyHaxball.API.Modules
{
    public class FilterModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(NotFoundFilter<,>)).InstancePerLifetimeScope();
        }
    }
}
