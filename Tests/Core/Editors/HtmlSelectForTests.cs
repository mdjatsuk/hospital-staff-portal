using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVC.Tests.Core.Editors;

[TestClass] public class HtmlSelectForTests : BaseTests
{
    protected override Type setType() => typeof(HtmlSelectFor);
    [TestMethod] public void SelectForTest()
    {
        var helper = new DummyHtmlHelper<MyModel2>();
        Expression<Func<MyModel2, string>> expr = m => m.Category;
        var list = new SelectList(new[] { "Cat A", "Cat B", "Cat C" });
        var result = HtmlSelectFor.SelectFor(helper, expr, list);
        notNull(result);
        using var writer = new StringWriter();
        result.WriteTo(writer, HtmlEncoder.Default);
        var renderedHtml = writer.ToString();
        var expectedHtml = "<dt class=\"col-sm-4\">Dummy Display Name</dt>" +
                   "<dd class=\"col-sm-8\"><select class='form-select'>" +
                   "<option>Cat A</option><option>Cat B</option><option>Cat C</option>" +
                   "</select><span class='text-danger'>Error</span></dd>";
        equal(expectedHtml, renderedHtml);
    }
}
public class MyModel2
{
    public string Category { get; set; }
}
