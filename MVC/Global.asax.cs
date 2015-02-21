using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Contracts;
using MVC.Controllers;

namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    [System.Runtime.InteropServices.GuidAttribute("B72F17EB-120A-4225-A5FA-8EE97064397F")]
    public class IoCConfig
    {

        /// <summary>
        /// For more info see 
        /// :https://code.google.com/p/autofac/wiki/MvcIntegration (mvc4 instructions)
        /// </summary>
        public static void RegisterDependencies()
        {
            #region Create the builder
            var builder = new ContainerBuilder();
            #endregion

            //#region Setup a common pattern
            //// placed here before RegisterControllers as last one wins
            //builder.RegisterAssemblyTypes()
            //       .Where(t => t.Name.EndsWith("Repository"))
            //       .AsImplementedInterfaces()
            //       .InstancePerRequest();
            //builder.RegisterAssemblyTypes()
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces()
            //       .InstancePerRequest();
            //#endregion

            #region Register modules
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            var domainAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            builder.RegisterAssemblyModules<CommonModule>(domainAssemblies.ToArray());

            #endregion


            #region Register all controllers for the assembly
            // Note that ASP.NET MVC requests controllers by their concrete types, 
            // so registering them As<IController>() is incorrect. 
            // Also, if you register controllers manually and choose to specify 
            // lifetimes, you must register them as InstancePerDependency() or 
            // InstancePerHttpRequest() - ASP.NET MVC will throw an exception if 
            // you try to reuse a controller instance for multiple requests. 
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
              .InstancePerRequest();

            #endregion

            builder.Register(c => new HomeController(c.ResolveNamed<IService>("ServiceUser")))
                   .InstancePerLifetimeScope();

            

            #region Model binder providers - excluded - not sure if need
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();
            #endregion

            #region Inject HTTP Abstractions
            /*
         The MVC Integration includes an Autofac module that will add HTTP request 
         lifetime scoped registrations for the HTTP abstraction classes. The 
         following abstract classes are included: 
        -- HttpContextBase 
        -- HttpRequestBase 
        -- HttpResponseBase 
        -- HttpServerUtilityBase 
        -- HttpSessionStateBase 
        -- HttpApplicationStateBase 
        -- HttpBrowserCapabilitiesBase 
        -- HttpCachePolicyBase 
        -- VirtualPathProvider 

        To use these abstractions add the AutofacWebTypesModule to the container 
        using the standard RegisterModule method. 
        */
            builder.RegisterModule<AutofacWebTypesModule>();

            #endregion

            #region Set the MVC dependency resolver to use Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

        }

    }

    //public class ServicesModule : CommonModule
    //{
    //    protected override void Load(ContainerBuilder builder)
    //    {
    //        builder.RegisterType<Services.ServiceUser>()
    //               .As<IService>()
    //               .InstancePerRequest();
    //    }
    //}
}
