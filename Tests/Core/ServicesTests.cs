using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Core;
using System;

namespace MVC.Tests.Core;

[TestClass, DoNotParallelize] public class ServicesTests : BaseTests
{
    protected override Type setType() => typeof(Services);
    public interface ITestService
    {
        string SayHello();
    }
    public class TestService : ITestService
    {
        public string SayHello() => "Hello";
    }
    public interface IUnregisteredTestService
    {
        string Message();
    }
    public class UnregisteredTestService : IUnregisteredTestService
    {
        public string Message() => "Success";
    }
    [TestInitialize] public void Setup()
    {
        Services.Clear();
        Services.init(new ServiceCollection());
    }
    [TestMethod] public void AddTest()
    {
        var service = new UnregisteredTestService();
        Services.Add(typeof(IUnregisteredTestService), service);
        var retrieved = Services.Get<IUnregisteredTestService>();
        notNull(retrieved);
        equal("Success", retrieved?.Message());
    }
    [TestMethod] public void RemoveTest()
    {
        var service = new TestService();
        Services.Add(typeof(ITestService), service);
        Services.Remove(typeof(ITestService));
        var retrieved = Services.Get<ITestService>();
        isNull(retrieved);
    }
    [TestMethod] public void ClearTest()
    {
        Services.Add(typeof(ITestService), new TestService());
        Services.Clear();
        var retrieved = Services.Get<ITestService>();
        isNull(retrieved);
    }
    [TestMethod] public void InitTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ITestService, TestService>();
        Services.init(serviceCollection);
        var retrieved = Services.Get<ITestService>();
        notNull(retrieved);
        equal("Hello", retrieved?.SayHello());
    }
    [TestMethod] public void GetTest_ReturnsServiceFromProviderTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ITestService, TestService>();
        Services.init(serviceCollection);
        var retrieved = Services.Get(typeof(ITestService)) as ITestService;
        notNull(retrieved);
        equal("Hello", retrieved?.SayHello());
    }
    [TestMethod] public void GetTest_ReturnsNullWhenServiceNotRegisteredTest()
    {
        Services.init(new ServiceCollection());
        var retrieved = Services.Get(typeof(ITestService)) as ITestService;
        isNull(retrieved);
    }
    [TestMethod] public void GetTest()
    {
        GetTest_ReturnsServiceFromProviderTest();
        GetTest_ReturnsNullWhenServiceNotRegisteredTest();
    }
}