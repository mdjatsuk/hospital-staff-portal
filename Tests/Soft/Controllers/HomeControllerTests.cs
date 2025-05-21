using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Soft.Controllers;
using MVC.Soft.Data;
using MVC.Soft.Models;
using System;
using System.Diagnostics;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class HomeControllerTests : BaseTests
{
    protected override Type setType() => typeof(HomeController);
    private class TestLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => null!;
        public bool IsEnabled(LogLevel logLevel) => false;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
    }
    private HomeController _controller;
    [TestInitialize] public void Setup()
    {
        _controller = new HomeController(new TestLogger<HomeController>());
    }
    [TestMethod] public void IndexTest()
    {
        var result = _controller.Index();
        isType(result, typeof(ViewResult));
    }
    [TestMethod] public void PrivacyTest()
    {
        var result = _controller.Privacy();
        isType(result, typeof(ViewResult));
    }
    [TestMethod] public void ErrorTest()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "test-trace-id";
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        var result = _controller.Error();
        var viewResult = result as ViewResult;
        notNull(viewResult);
        var model = viewResult.Model as ErrorViewModel;
        notNull(model);
        equal("test-trace-id", model.RequestId);
    }
}
