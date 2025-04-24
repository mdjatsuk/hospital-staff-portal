using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace MVC.Core.Editors;

public static class HtmlForInput
{
    public static IHtmlContent ForInput<TModel, TResult>(
        this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, IHtmlContent editor) =>
        h.ForShow(e,
            typeof(TResult) == typeof(bool) || typeof(TResult) == typeof(bool?)
                ? h.EditorFor(e, new { htmlAttributes = new { @class = "form-check-input" } })
                : editor,
            h.ValidationMessageFor(e, "", new { @class = "text-danger" }));
}