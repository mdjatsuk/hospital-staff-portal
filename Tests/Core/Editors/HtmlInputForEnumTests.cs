using MVC.Core.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVC.Tests.Core.Editors;

[TestClass] public class HtmlInputForEnumTests : BaseTests
{
    protected override Type setType() => typeof(HtmlInputForEnum);
    [TestMethod] public void InputForEnumTest()
    {
        var helper = new DummyHtmlHelper<MyModel1>();
        Expression<Func<MyModel1, Status>> expr = m => m.Status;
        var result = HtmlInputForEnum.InputForEnum(helper, expr);
        notNull(result);
        using var writer = new StringWriter();
        result.WriteTo(writer, HtmlEncoder.Default);
        var renderedHtml = writer.ToString();
        var expectedHtml = "<dt class=\"col-sm-4\">Dummy Display Name</dt>" +
                           "<dd class=\"col-sm-8\"><select class='form-select'>" +
                           "<option>Active</option><option>Inactive</option><option>Suspended</option>" +
                           "</select><span class='text-danger'>Error</span></dd>";
        equal(expectedHtml, renderedHtml);
    }
}
public enum Status
{
    Active,
    Inactive,
    Suspended
}
public class MyModel1
{
    public Status Status { get; set; }
}
