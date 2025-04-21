using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.Core.Editors;
using System.Linq.Expressions;

namespace MVC_Project.Core.Editors;

public static class HtmlInputFor
{
    public static IHtmlContent InputFor<TModel, TResult>
        (this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, object htmlAttributes = null)
        => h.ForInput(e, h.EditorFor(e, new { htmlAttributes = new { @class = "form-control" } }));
}
