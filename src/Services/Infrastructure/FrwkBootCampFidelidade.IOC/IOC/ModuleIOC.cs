using Autofac;

namespace FrwkBootCampFidelidade.Infraestrutura.IOC.IOC
{
    public class ModuleIOC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationIOC.Load(builder);
        }
    }
}
