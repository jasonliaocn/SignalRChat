using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRChat.Common;
using System.Reflection;
using System.Web.Mvc;

namespace SignalRChat
{
    public class IoCMvcConfig
    {
        public static void Initialize(HubConfiguration config)
        {
            var builder = new ContainerBuilder();
            Register(builder);

            var container = builder.Build();

            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            //GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
            config.Resolver = new AutofacDependencyResolverForSignalR(container);
        }

        private static void Register(ContainerBuilder builder)
        {
            //Register current AppDomain's components here.
            var executingAssemblies = Assembly.GetExecutingAssembly();

            builder.RegisterControllers(executingAssemblies);
            // Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterAssemblyTypes(executingAssemblies)
            .Where(t => t.Name.EndsWith("Manager") || t.Name.EndsWith("Client"))
            .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(executingAssemblies)
            .Where(t => typeof(Microsoft.AspNet.SignalR.Hubs.IHub).IsAssignableFrom(t))
            .ExternallyOwned();

        }
    }
}