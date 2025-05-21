using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using MVC.Core.Editors;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;

namespace MVC.Tests.Core.Editors;

[TestClass, DoNotParallelize] public class HtmlTableForTests : BaseTests
{
    protected override Type setType() => typeof(HtmlTableFor);
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double AppointmentFee { get; set; }
        public string EmptyField { get; set; }
    }
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        ResetIsRelated();
    }
    [TestCleanup] public override void Cleanup()
    {
        base.Cleanup();
        ResetIsRelated();
    }
    private void ResetIsRelated()
    {
        var field = typeof(HtmlTableFor).GetField("isRelated", BindingFlags.NonPublic | BindingFlags.Static);
        if (field != null) field.SetValue(null, false);
    }
    private DummyHtmlHelperIEnumerable<TestModel> CreateHelper(
        string? orderBy = null, string? filter = null, int? selectedId = null, int? pageIdx = null)
    {
        var helper = new DummyHtmlHelperIEnumerable<TestModel>();
        helper.ViewBag.OrderBy = orderBy;
        helper.ViewBag.Filter = filter;
        helper.ViewBag.SelectedId = selectedId;
        helper.ViewBag.PageIdx = pageIdx;
        helper.ViewContext.RouteData.Values["controller"] = "TestController";
        return helper;
    }
    private string GetString(IHtmlContent content)
    {
        using var writer = new System.IO.StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
    [TestMethod] public void TableForTest()
    {
        ResetIsRelated();
        var helper = CreateHelper(orderBy: "Name", filter: "filterValue", selectedId: 1, pageIdx: 0);
        var items = new List<TestModel>
        {
            new TestModel { Id = 1, Name = "Alice", AppointmentFee = 100.5, Date = new DateTime(2025, 5, 20) },
            new TestModel { Id = 2, Name = "Bob", AppointmentFee = 200.75, Date = new DateTime(2025, 6, 15) }
        };
        var result = HtmlTableFor.TableFor(helper, items, hasSelect: true, "Name", "AppointmentFee", "Date");
        var html = GetString(result);
        isTrue(html.Contains("<table"));
        isTrue(html.Contains("Alice"));
        isTrue(html.Contains("Bob"));
        isTrue(html.Contains("100.50"));
        isTrue(html.Contains("5/20/2025"));
        isTrue(html.Contains("Actions"));
        isTrue(html.Contains("href='/TestController/Edit/1'"));
        isTrue(html.Contains("Name ▲"));
        isTrue(html.Contains("table-success"));
    }
    [TestMethod] public void TableForTest1()
    {
        ResetIsRelated();
        var helper = CreateHelper();
        var items = new List<TestModel>
        {
            new TestModel { Id = 1, Name = "Charlie", AppointmentFee = 123.45 },
            new TestModel { Id = 2, Name = "Dana", AppointmentFee = 67.89 }
        };
        var result = HtmlTableFor.TableFor(helper, items, hasSelect: true, "Name");
        var html = GetString(result);
        isTrue(html.Contains("Charlie"));
        isTrue(html.Contains("Dana"));
        isTrue(html.Contains("Actions"));
    }
    [TestMethod] public void RelatedTableTest()
    {
        var helper = CreateHelper();
        var items = new List<TestModel>
        {
            new TestModel { Id = 3, Name = "Eve" },
            new TestModel { Id = 4, Name = "Frank" }
        };
        var result = HtmlTableFor.RelatedTable(helper, items, "Name");
        var html = GetString(result);
        isTrue(html.Contains("Eve"));
        isTrue(html.Contains("Frank"));
        isFalse(html.Contains("Actions"));
        isTrue(html.Contains("table-secondary"));
    }
    [TestMethod] public void TableForTest2()
    {
        var helper = CreateHelper();
        var items = new List<TestModel>
        {
            new TestModel { Id = 5, Name = "", AppointmentFee = 0, EmptyField = "" },
            new TestModel { Id = 6, Name = "Grace", AppointmentFee = 150.0 }
        };
        var result = HtmlTableFor.TableFor(helper, items, hasSelect: true, "Name", "AppointmentFee", "EmptyField");
        var html = GetString(result);
        isTrue(html.Contains("Grace"));
        isFalse(html.Contains(">5<"));
    }
}