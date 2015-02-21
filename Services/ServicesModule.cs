using System;
using System.Linq;
using Autofac;
using Contracts;

namespace Services
{
    public class ServicesModule : CommonModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceUser>()
                   .As<IService>()
                   .InstancePerRequest();
        }
    }
}
