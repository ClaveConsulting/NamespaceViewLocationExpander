namespace Clave.NamespaceViewLocationExpander
{
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IServiceCollection UseNamespaceViewLocations(
            this IServiceCollection services,
            string prefix = null)
        {
            prefix = prefix ?? Assembly.GetCallingAssembly().GetName().Name;
            services.Configure<RazorViewEngineOptions>(config =>
            {
                config.ViewLocationFormats.Insert(0, "/{namespace}/{0}.cshtml");
                config.AreaViewLocationFormats.Insert(0, "/{namespace}/{0}.cshtml");
                config.ViewLocationExpanders.Insert(0, new NamespaceViewLocationExpander(prefix));
            });

            return services;
        }
    }
}