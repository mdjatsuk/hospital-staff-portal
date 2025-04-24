using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Editors;
using System.Linq.Expressions;

namespace MVC.Core.Editors;

public static class HtmlShowFor
{
    public static IHtmlContent ShowFor<TModel, TResult>(
        this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e)
        => h.ForShow(e, h.DisplayFor(e));
}
