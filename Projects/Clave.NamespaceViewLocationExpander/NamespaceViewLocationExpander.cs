namespace Clave.NamespaceViewLocationExpander
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Razor;

    public class NamespaceViewLocationExpander : IViewLocationExpander
    {
        private readonly string _prefix;

        public NamespaceViewLocationExpander(string prefix = null)
        {
            _prefix = prefix ?? Assembly.GetCallingAssembly().GetName().Name;
        }

        public IEnumerable<string> ExpandViewLocations(
        ViewLocationExpanderContext context,
        IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(nameof(NamespaceViewLocationExpander), out var value))
            {
                return viewLocations
                    .Select(l => l.Replace("{namespace}", NamespaceToPath(value)));
            }
            else
            {
                return viewLocations;
            }
        }

        public string NamespaceToPath(string name)
        {
            return name
                ?.Replace(_prefix, string.Empty)
                .Replace('.', '/')
                .TrimStart('/');
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context.ActionContext.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                context.Values[nameof(NamespaceViewLocationExpander)] = actionDescriptor.ControllerTypeInfo.Namespace;
            }
        }
    }
}
