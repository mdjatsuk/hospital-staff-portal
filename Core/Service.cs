using Microsoft.Extensions.DependencyInjection;

namespace MVC.Core;

public static class Services
{
    internal static Dictionary<Type, object> services { get; private set; } = new Dictionary<Type, object>();
    private static IServiceProvider? sp;
    internal static void init(IServiceCollection c) => sp = c?.BuildServiceProvider();
    public static T? Get<T>() => (T?)Get(typeof(T));
    public static object? Get(Type t)
    {
        if (sp != null)
        {
            try
            {
                return sp.GetRequiredService(t);
            }
            catch
            {
                
            }
        }
        return fromList(t);
    }
    private static object? fromList(Type t)
    {
        services.TryGetValue(t, out var result);
        return result;
    }
    public static void Add(Type serviceType, object implementation)
    {
        if (services.ContainsKey(serviceType)) services[serviceType] = implementation;
        else services.Add(serviceType, implementation);
    }
    public static void Remove(Type serviceType)
    {
        services.Remove(serviceType);
    }
    public static void Clear() => services.Clear();
}