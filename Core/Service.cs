using Microsoft.Extensions.DependencyInjection;

namespace MVC.Core;

public static class Services
{
    internal static Dictionary<Type, object> services { get; set; } = [];
    private static IServiceProvider? sp;
    internal static void init(IServiceCollection c) => sp = c?.BuildServiceProvider();
    public static T? Get<T>() => Get(typeof(T));
    public static dynamic? Get(Type t) => sp?.GetRequiredService(t) ?? fromList(t);
    private static object? fromList(Type t)
    {
        object? result;
        services.TryGetValue(t, out result);
        return result;
    }
}