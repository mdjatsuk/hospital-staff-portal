using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Core.Editors;
public static class HtmlTableFor
{
    public static IHtmlContent TableFor<TModel>(this IHtmlHelper<IEnumerable<TModel>> h, IEnumerable<TModel> list)
    {
        var properties = GetProperties<TModel>();
        var table = CreateTable();
        table.InnerHtml.AppendHtml(CreateHeader(h, properties));
        table.InnerHtml.AppendHtml(CreateBody(h, properties, list));
        return table;
    }

    private static PropertyInfo[] GetProperties<TModel>()
        => typeof(TModel).GetProperties().Where(p => p.Name != "Id").ToArray();

    private static TagBuilder CreateTable()
    {
        var table = new TagBuilder("table");
        table.AddCssClass("table");
        return table;
    }

    private static IHtmlContent CreateHeader<TModel>(IHtmlHelper<IEnumerable<TModel>> h, PropertyInfo[] properties)
    {
        var thead = new TagBuilder("thead");
        var row = new TagBuilder("tr");

        foreach (var property in properties)
        {
            var column = new TagBuilder("th");
            var displayName = GetDisplayName(property);
            column.InnerHtml.AppendHtml(displayName);
            row.InnerHtml.AppendHtml(column);
        }

        var actionColumn = new TagBuilder("th");
        actionColumn.InnerHtml.Append("Actions");
        row.InnerHtml.AppendHtml(actionColumn);


        thead.InnerHtml.AppendHtml(row);
        return thead;
    }

    private static string GetDisplayName(PropertyInfo property)
        => property.GetCustomAttribute<DisplayAttribute>()?.Name ?? property.Name;

    private static IHtmlContent CreateBody<TModel>(IHtmlHelper<IEnumerable<TModel>> h, PropertyInfo[] properties, IEnumerable<TModel> list)
    {
        var tbody = new TagBuilder("tbody");

        foreach (var item in list)
        {
            var row = new TagBuilder("tr");

            foreach (var p in properties)
            {
                var td = new TagBuilder("td");
                var value = getValue(p, item);
                td.InnerHtml.Append(value?.ToString());
                row.InnerHtml.AppendHtml(td);
            }

            var controllerName = h.ViewContext.RouteData.Values["controller"].ToString();
            var tdActions = new TagBuilder("td");
            var itemId = item.GetType().GetProperty("Id")?.GetValue(item)?.ToString();
            if (itemId != null)
            {
                tdActions.InnerHtml.AppendHtml($"<a href='/{controllerName}/Edit/{itemId}'>Edit</a> | ");
                tdActions.InnerHtml.AppendHtml($"<a href='/{controllerName}/Details/{itemId}'>Details</a> | ");
                tdActions.InnerHtml.AppendHtml($"<a href='/{controllerName}/Delete/{itemId}'>Delete</a>");
            }

            row.InnerHtml.AppendHtml(tdActions);
            tbody.InnerHtml.AppendHtml(row);
        }

        return tbody;
    }

    private static IHtmlContent getValue<TModel>(PropertyInfo? p, TModel? item)
    {
        var v = p?.GetValue(item);
        var dt = v as DateTime?;
        if (dt != null) return new HtmlString(dt?.ToShortDateString() ?? "");
        if (p?.Name == "Price" && v is double price) return new HtmlString($"{price:C}");
        return new HtmlString(v?.ToString() ?? "");
    }
}
