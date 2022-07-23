using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace FinLib.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypesOf<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsSubclassOf(typeof(T))
                                        && !x.IsAbstract
                                        && !x.IsGenericType));

            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(type, type, lifetime));
            }
        }
    }
}
