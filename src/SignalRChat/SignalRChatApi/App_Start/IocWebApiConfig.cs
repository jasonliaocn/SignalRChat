using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace SignalRChatApi
{
    public class IocWebApiConfig
    {
        public static void Initialize(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            Register(builder);

            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            config.DependencyResolver = resolver;
        }

        private static void Register(ContainerBuilder builder)
        {
            //Register current AppDomain's components here.

            var executingAssemblies = Assembly.GetExecutingAssembly();

            builder.RegisterType<SignalRChatApi.Providers.ApplicationOAuthProvider>();

            builder.RegisterApiControllers(executingAssemblies);

            builder.RegisterAssemblyTypes(executingAssemblies)
                    .Where(t => t.Name.EndsWith("Mapper") || t.Name.EndsWith("Repository") || t.Name.EndsWith("BO"))
                    .AsImplementedInterfaces();
        }
    }
}