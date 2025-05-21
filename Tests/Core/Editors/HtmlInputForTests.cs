using MVC.Core.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVC.Tests.Core.Editors;

[TestClass] public class HtmlInputForTests : BaseTests
{
    protected override Type setType() => typeof(HtmlInputFor);
    [TestMethod] public void InputForTest()
    {
        var helper = new DummyHtmlHelper<MyModel>();
        Expression<Func<MyModel, bool>> expr = m => m.IsActive;
        var result = HtmlInputFor.InputFor(helper, expr);
        notNull(result);
        using var writer = new StringWriter();
        result.WriteTo(writer, HtmlEncoder.Default);
        var renderedHtml = writer.ToString();
        var expectedHtml = "<dt class=\"col-sm-4\">Dummy Display Name</dt>" +
                           "<dd class=\"col-sm-8\"><input type='checkbox' class='form-check-input' />" +
                           "<span class='text-danger'>Error</span></dd>";
        equal(expectedHtml, renderedHtml);
    }
}
