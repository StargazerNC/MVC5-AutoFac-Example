using System;
using System.Linq;
using Autofac;
using Contracts;
using Repository;

namespace Repositories
{
    class RespositoriesModule : CommonModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                   .As<IRepository>()
                   .InstancePerRequest();
        }
    }
            
}
