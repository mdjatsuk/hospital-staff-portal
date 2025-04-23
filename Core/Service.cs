using Microsoft.Extensions.DependencyInjection;

namespace MVC.Core;

public class Services
{
    private static IServiceProvider? sp;
    internal static void init(IServiceCollection c) => sp = c?.BuildServiceProvider();
    public static T? Get<T>() => Get(typeof(T));
    public static dynamic? Get(Type t) => sp?.GetRequiredService(t);
}