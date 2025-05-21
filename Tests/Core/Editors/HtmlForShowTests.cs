using Microsoft.AspNetCore.Html;
using MVC.Core.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVC.Tests.Core.Editors;

[TestClass] public class HtmlForShowTests : BaseTests
{
    protected override Type setType() => typeof(HtmlForShow);
    [TestMethod] public void ForShowTest()
    {
        var helper = new DummyHtmlHelper<MyModel>();
        Expression<Func<MyModel, bool>> expr = m => m.IsActive;
        var customControl = new HtmlString("<span class='custom-control'>Control</span>");
        var result = HtmlForShow.ForShow(helper, expr, customControl);
        notNull(result);
        using var writer = new StringWriter();
        result.WriteTo(writer, HtmlEncoder.Default);
        var renderedHtml = writer.ToString();
        string expectedHtml = "<dt class=\"col-sm-4\">Dummy Display Name</dt><dd class=\"col-sm-8\"><span class='custom-control'>Control</span></dd>";
        equal(expectedHtml, renderedHtml);
    }
}
