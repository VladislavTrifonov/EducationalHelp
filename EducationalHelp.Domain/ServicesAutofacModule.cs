using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace EducationalHelp.Services
{
    public class ServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Service")).AsSelf().InstancePerLifetimeScope();
        }
    }
}
