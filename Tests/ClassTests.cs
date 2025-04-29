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
public abstract class SealedTests<TClass, TBaseClass> : ClassTests<TClass, TBaseClass>
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
    [TestMethod] public void IsBaseTypeOfTest() => equal(typeof(TClass).BaseType, typeof(TBaseClass));
    protected override void canGet<T>(PropertyInfo pi, T? expected) where T : default
    {
        var actual = pi.GetValue(obj);
        equal(actual, expected);
    }

    protected override void canSet<T>(PropertyInfo pi, T? v)
        where T : default => pi.SetValue(obj, v);
}

