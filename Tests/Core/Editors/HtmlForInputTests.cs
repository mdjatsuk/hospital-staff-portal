using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Core.Editors;
using System;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Web.Mvc;
using System.IO;

namespace MVC.Tests.Core.Editors;

[TestClass] public class HtmlForInputTests : BaseTests
{
    protected override Type setType() => typeof(HtmlForInput);
    [TestMethod] public void ForInputTest()
    {
        var helper = new DummyHtmlHelper<MyModel>();
        Expression<Func<MyModel, bool>> expr = m => m.IsActive;
        var editor = new HtmlString("<input />");
        var result = HtmlForInput.ForInput(helper, expr, editor);
        notNull(result);
        using (var writer = new StringWriter())
        {
            result.WriteTo(writer, HtmlEncoder.Default);
            var renderedHtml = writer.ToString();
            string expectedHtml = "<dt class=\"col-sm-4\">Dummy Display Name</dt><dd class=\"col-sm-8\"><input type='checkbox' class='form-check-input' /><span class='text-danger'>Error</span></dd>";
            equal(expectedHtml, renderedHtml);
        }
    }
}
public class MyModel
{
    public bool IsActive { get; set; }
}