using Microsoft.AspNetCore.Routing.Matching;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Reflection;

namespace MVC.Tests;

public abstract class BaseTests
{
    protected Type? type;
    protected abstract Type setType();
    [TestInitialize] public virtual void Initialize() => type = setType();
    [TestCleanup] public virtual void Cleanup() => type = null;

    protected const int repeatCount = 1000;

    [TestMethod] public virtual void IsTested()
    {
        var testMethods = GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null)
            .Select(m => m.Name).ToArray();

        var members = type!
            .GetMembers(BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly)
            .Select(m => m.Name)
            .Where(m => !m.Contains("get_") && !m.Contains("set_") && !m.Contains(".ctor"))
            .Where(m => !testMethods.Contains(m + "Test"))
            .ToArray();

        if (members.Length == 0) return;
        var notTestedMembers = string.Join(", ", members);
        if (members.Length == 1)
            notTested($"Test method for <{notTestedMembers}> not found.");
        notTested($"Test methods for <{notTestedMembers}> not found.");
    }
    protected void equal<T>(T? x, T? y, string? msg = null) => Assert.AreEqual(x, y, msg);
    protected void notEqual<T>(T? x, T? y, string? msg = null) => Assert.AreNotEqual(x, y, msg);
    protected void isTrue(bool x, string? msg = null) => Assert.IsTrue(x, msg);
    protected void isFalse(bool x, string? msg = null) => Assert.IsFalse(x, msg);
    protected void isNull<T>(T? x, string? msg = null) => Assert.IsNull(x, msg);
    protected void notNull<T>(T? x, string? msg = null) => Assert.IsNotNull(x, msg);
    protected void isType(object? x, Type t, string? msg = null) => Assert.IsInstanceOfType(x, t, msg);
    protected void notType(object? x, Type t, string? msg = null) => Assert.IsNotInstanceOfType(x, t, msg);
    protected void notTested(string? msg = null) => Assert.Inconclusive(msg);
    protected void same<T>(T? x, T? y, string? msg = null) => Assert.AreSame(x, y, msg);
    protected void notSame<T>(T? x, T? y, string? msg = null) => Assert.AreNotSame(x, y, msg);
    protected void fail(string? msg = null) => Assert.Fail(msg);
    protected void isProperty<T>(bool isReadOnly = false)
    {
        var pi = getPropertyInfo();
        notNull(pi);
        isType<T>(pi!);
        canRead(pi!);
        if (!isReadOnly) canWrite(pi!);
        var v = MVC.Aids.Random.Type<T>();
        canSet(pi!, v);
        canGet(pi!, v);
    }

    protected PropertyInfo? getPropertyInfo()
    {
        var n = getPropertyName();
        if (n is null) return null;
        return type?.GetProperty(n);
    }

    protected static string? getPropertyName()
    {
        var stack = new StackTrace();
        for(var i = 1; i < stack.FrameCount; i++)
        {
            var m = stack.GetFrame(i)?.GetMethod();
            if (m is null) continue;
            var isTest = m.GetCustomAttributes(typeof(TestMethodAttribute), true).Any();
            if (isTest) return m.Name.Replace("Test", string.Empty);
        }
        return null;
    }
    private void isType<T>(PropertyInfo pi)
    {
        if (pi!.PropertyType?.Name != typeof(T).Name)
        {
            fail($"Property <{pi.Name}> is not a type of <{typeof(T).Name}>.");
        }
    }
    protected void canRead(PropertyInfo pi) => isTrue(pi.CanRead, $"Property '{pi.Name}' does not have a getter.");
    protected void canWrite(PropertyInfo pi) => isTrue(pi.CanWrite, $"Property '{pi.Name}' does not have a setter.");
    protected virtual void canSet<T>(PropertyInfo pi, T? v) => fail();
    protected virtual void canGet<T>(PropertyInfo pi, T? v) => fail();
}

public abstract class EnumTests<TEnum>(int count) : BaseTests where TEnum : Enum
{
    protected override Type setType() => typeof(TEnum);
    protected string[] membersName { get; set; } = getNames();

    private static string[] getNames()
    {
        var t = typeof(TEnum);
        var fields = t.GetFields(BindingFlags.Public | BindingFlags.Static);
        var names = fields.Select(f => f.Name);
        var result = names.ToArray();
        return result;
    }

    [TestMethod] public void CountTest() => equal(membersName.Length, count);

    [TestMethod]
    public override void IsTested()
    {
        var testMethods = GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null)
            .Select(m => m.Name).ToArray();
        notNull(type);
        var members = membersName
            .Where(name => !testMethods.Contains(name + "Test"))
            .ToArray();
        if (members.Length == 0) return;
        var notTestedMembers = string.Join(", ", members);
        if (members.Length == 1)
            notTested($"Test method for <{notTestedMembers}> not found.");
        notTested($"Test methods for <{notTestedMembers}> not found.");
    }

    protected void isEnum(int value)
    {
        var name = getPropertyName();
        notNull(name);
        var actual = Enum.Parse(typeof(TEnum), name!);
        equal((int)actual, value);
    }
}
