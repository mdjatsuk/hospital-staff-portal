using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests;

public abstract class ClassTests<TClass, TBaseClass> : BaseClassTests<TClass, TBaseClass>
    where TClass : class, new()
    where TBaseClass : class
{
    protected override TClass createObj() => new();
}
public abstract class SealedTests<TClass, TBaseClass> :ClassTests<TClass, TBaseClass>
    where TClass : class, new()
    where TBaseClass : class
{
    [TestMethod] public void IsSealedTest() => isTrue(typeof(TClass).IsSealed);
}
public abstract class AbstractTests<TClass, TBaseClass> : BaseClassTests<TClass, TBaseClass>
    where TClass : class
    where TBaseClass : class
{
    [TestMethod] public void IsAbstractTest() => isTrue(typeof(TClass).IsAbstract);
}
public abstract class BaseClassTests<TClass, TBaseClass> : BaseTests
    where TClass : class
    where TBaseClass : class
{
    protected TClass? obj;
    protected abstract TClass createObj();
    protected override Type setType() => typeof(TClass);
    [TestInitialize] public virtual void Initialize()
    {
        base.Initialize();
        obj = createObj();
    }
    [TestCleanup] public virtual void Cleanup()
    {
        base.Cleanup();
        obj = null;;
    }
    [TestMethod] public void CanCreateTest() => notNull(obj);
    [TestMethod] public void IsTypeOfTest() => isType(obj, typeof(TClass));
    [TestMethod] public void IsBaseTypeOfTest() => equal(obj?.GetType().BaseType, typeof(TBaseClass));
    [TestMethod]
    public void IsTested()
    {
        var testMethods = GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null)
            .Select(m => m.Name).ToArray();

        var members = typeof(TClass)
            .GetMembers(BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly)
            .Select(m => m.Name)
            .Where(m => !m.Contains("get_") && !m.Contains("set_") && !m.Contains(".ctor"))
            .Where(m => !testMethods.Contains(m+"Test"))
            .ToArray();

        if (members.Length == 0) return;
        var notTestedMembers = string.Join(", ", members);
        if (members.Length == 1)
            notTested($"Test method for <{notTestedMembers}> not found.");
        notTested($"Test methods for <{notTestedMembers}> not found.");
    }

    protected override void canGet<T>(PropertyInfo pi, T? expected) where T : default
    {
        var actual = pi.GetValue(obj);
        equal(actual, expected);
    }

    protected override void canSet<T>(PropertyInfo pi, T? v)
        where T : default => pi.SetValue(obj, v);
}

