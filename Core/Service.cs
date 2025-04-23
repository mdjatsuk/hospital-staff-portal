using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Core
{
    public class Service
    {
        private static IServiceProvider? sp;
        internal static void init(IServiceCollection c) => sp = c?.BuildServiceProvider();
        public static T? Get<T>() => Get(typeof(T));
        public static dynamic? Get(Type t) => sp?.GetRequiredService(t);
    }
}
