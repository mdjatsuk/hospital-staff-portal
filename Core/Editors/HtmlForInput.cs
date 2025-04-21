using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Core.Editors
{
    public static class HtmlForInput
    {
        public static IHtmlContent ForInput<TModel, TResult>(
         this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, IHtmlContent editor)
            => h.ForShow(e, editor,
                h.ValidationMessageFor(e, "", new { @class = "text-danger" }));
    }
}

