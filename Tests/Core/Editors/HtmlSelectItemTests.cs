using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static MVC.Tests.Core.Editors.HtmlTableForTests;

namespace MVC.Tests.Core.Editors;

[TestClass] public class HtmlSelectItemTests : BaseTests
{
    protected override Type setType() => typeof(HtmlSelectItem);
    [TestMethod] public void SelectItemTest()
    {
        var helper = new DummyHtmlHelper<MyModel3>();
        Expression<Func<MyModel3, int>> expr = m => m.CategoryId;
        var selectList = new SelectList(new[]
        {
            new SelectListItem { Text = "Cat A", Value = "1" },
            new SelectListItem { Text = "Cat B", Value = "2" }
        }, "Value", "Text");
        var result = HtmlSelectItem.SelectItem(helper, expr, selectList);
        using var writer = new StringWriter();
        result.WriteTo(writer, HtmlEncoder.Default);
        var renderedHtml = writer.ToString();
        var expectedHtml =
            "<div class=\"form-group\">" +
            "<label class=\"control-label\" for=\"CategoryId\">CategoryId</label>" +
            "<select class='form-select'>" +
            "<option>--Select Dummy Display Name--</option><option>Cat A</option><option>Cat B</option>" +
            "</select>" +
            "<span class='text-danger'>Error</span>" +
            "</div>";
        equal(expectedHtml, renderedHtml);
    }
    [TestMethod] public void SelectItemTest1()
    {
        var helper = new DummyHtmlHelper<MyModel3>();
        Expression<Func<MyModel3, int>> expr = m => m.CategoryId;
        var result = HtmlSelectItem.SelectItem(helper, expr, "product");
        using var writer = new StringWriter();
        result.WriteTo(writer, HtmlEncoder.Default);
        var renderedHtml = writer.ToString();
        var expectedHtml =
            "<div class=\"form-group\">" +
            "<label class=\"control-label\" for=\"CategoryId\">CategoryId</label>" +
            "<select name=\"CategoryId\" class=\"selectItems2 form-control\" data-controller=\"product\" data-id=\"42\">" +
            "</select>" +
            "<span class='text-danger'>Error</span>" +
            "</div>";
        equal(expectedHtml, renderedHtml);
    }
    public class MyModel3
    {
        public int CategoryId { get; set; } = 42;
    }
    [TestMethod]
    public void ConstantsTest()
    {
        equal("Select", HtmlSelectItem.Constants.Select, "Constants.Select should be 'Select'");
    }
}
