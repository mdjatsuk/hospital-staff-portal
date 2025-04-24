using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Core.Editors;
public static class HtmlInputForEnum
{
    public static IHtmlContent InputForEnum<TModel, TResult>(
        this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) =>
        h.SelectFor(e, selectList<TResult>());


    private static SelectList selectList<TEnum>()
    {
        var t = typeof(TEnum);
        var x = Nullable.GetUnderlyingType(t);
        if (x != null) t = x;
        return new SelectList(Enum.GetNames(t));
    }
}