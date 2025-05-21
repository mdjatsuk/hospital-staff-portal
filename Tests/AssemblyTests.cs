using MVC.Tests;
using System.Reflection;

namespace Mvc.Tests;

public abstract class AssemblyTests(string namespaceName) : BaseTests
{
    protected override Type? setType() => null;
    [TestMethod] public override void IsTested()
    {
        var testNamespace = string.Empty;
        if (namespaceName.StartsWith("MVC"))
            testNamespace = namespaceName.Replace("MVC", "MVC.Tests");
        else testNamespace = "MVC.Tests." + namespaceName;
        var testAssembly = Assembly.GetExecutingAssembly();
        var testClasses = testAssembly
            .GetTypes()
            .Where(t => (t?.Namespace is not null) && t.Namespace.StartsWith(testNamespace))
            .Select(t => t.Name)
            .ToArray();

        var domain = AppDomain.CurrentDomain;
        var assemblies = domain.GetAssemblies();
        var assemblyName = namespaceName.Replace("Mvc", "");
        var assembly = assemblies
            .FirstOrDefault(a => a.FullName?.StartsWith(namespaceName) ?? false);
        if (assembly == null) assembly = assemblies
            .FirstOrDefault(a => a.FullName?.StartsWith(assemblyName) ?? false);
        if (assembly == null) notTested($"Assembly {namespaceName} not found.");

        var classes = assembly?
            .GetTypes()
            .Where(t => !t.IsInterface && t.IsPublic && !t.IsEnum)
            .Where(t => {
                var publicMembers = t.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
                return publicMembers.Any(m =>
                    m.MemberType != MemberTypes.Property &&
                    m.MemberType != MemberTypes.Constructor &&
                    !(m.MemberType == MemberTypes.Method && ((MethodInfo)m).IsSpecialName)
                );
            })
            .Select(t => t.Name)
            .Select(t => {
                var i = t.IndexOf('`');
                return i > 0 ? t.Substring(0, i) : t;
            })
            .Where(t => !t.Contains("Model") && !t.Contains("ForgotPasswordConfirmation") && !t.Contains("ManageNavPages")
            && !t.Contains("EntityView"))
            .Distinct()
            .Where(t => !testClasses.Contains(t + "Tests")).ToArray();

        if (classes?.Length == 0) return;
        var notTestedClasses = string.Join(", ", classes ?? []);
        if (classes?.Length == 1)
            notTested($"Test class for <{notTestedClasses}> not found.");
        notTested($"Test classes for <{notTestedClasses}> not found.");
    }
}
