using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Core.Editors;
public static class HtmlTableFor
{
    private static bool isRelated;

    public static IHtmlContent TableFor<TModel>(this IHtmlHelper<IEnumerable<TModel>> h, IEnumerable<TModel> list, bool hasSelect = false,
        params string[] propsToShow)
    {
        var props = GetProperties<TModel>();
        if (propsToShow.Length > 0)
            props = props.Where(p => propsToShow.Contains(p.Name)).ToArray();

        var table = CreateTable();
        table.InnerHtml.AppendHtml(CreateHeader(h, props));
        table.InnerHtml.AppendHtml(CreateBody(h, props, list, hasSelect));
        return table;
    }

    public static IHtmlContent TableFor<TModel>(this IHtmlHelper<IEnumerable<TModel>> h, IEnumerable<TModel> list,
        params string[] propsToShow)
        => h.TableFor(list, false, propsToShow);

    public static IHtmlContent RelatedTable<TModel>(
        this IHtmlHelper<IEnumerable<TModel>> h, IEnumerable<TModel> list,
        params string[] propsToShow)
    {
        isRelated = true;
        var result = h.TableFor(list, false, propsToShow);
        isRelated = false;
        return result;
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
        if (isRelated) row.AddCssClass("table-secondary");

        foreach (var property in properties)
        {
            var column = new TagBuilder("th");
            var displayName = GetDisplayName(property);
            displayName = UpdateDisplayName(h, displayName, property.Name);
            var sortOrder = UpdateSortOrder(h, property.Name);
            var link = $"<a class=\"header-link\" href=\"?pageIdx=0&orderBy={sortOrder}&filter={h.ViewBag.Filter}\">{displayName}</a>";
            column.InnerHtml.AppendHtml(isRelated ? displayName : link);
            row.InnerHtml.AppendHtml(column);
        }

        if (!isRelated)
        {
            var actionColumn = new TagBuilder("th");
            actionColumn.InnerHtml.Append("Actions");
            row.InnerHtml.AppendHtml(actionColumn);
        }

        thead.InnerHtml.AppendHtml(row);
        return thead;
    }

    private static string UpdateDisplayName<TModel>(IHtmlHelper<IEnumerable<TModel>> h, string name, string propName)
    {
        var sortOrder = h.ViewBag.OrderBy as string;
        if (sortOrder == propName) name += " ▲";
        else if (sortOrder == propName + "_desc") name += " ▼";
        else if (!isRelated) name += " ◀";
        return name;
    }

    private static string UpdateSortOrder<TModel>(IHtmlHelper<IEnumerable<TModel>> h, string propName)
    {
        var sortOrder = h.ViewBag.OrderBy as string;
        return sortOrder == propName ? propName + "_desc" : propName;
    }

    private static string GetDisplayName(PropertyInfo property)
        => property.GetCustomAttribute<DisplayAttribute>()?.Name ?? property.Name;

    private static IHtmlContent CreateBody<TModel>(
        IHtmlHelper<IEnumerable<TModel>> h, PropertyInfo[] properties,
        IEnumerable<TModel> list, bool hasSelect)
    {
        var tbody = new TagBuilder("tbody");

        foreach (var item in list)
        {
            var row = new TagBuilder("tr");
            var itemId = item.GetType().GetProperty("Id")?.GetValue(item)?.ToString();

            if (!isRelated && itemId == h.ViewBag.SelectedId?.ToString())
                row.AddCssClass("table-success");

            bool hasNonEmptyValue = false;
            foreach (var p in properties)
            {
                var td = new TagBuilder("td");
                var value = getValue(p, item);
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    hasNonEmptyValue = true;
                }
                td.InnerHtml.AppendHtml(value);
                row.InnerHtml.AppendHtml(td);
            }

            if (hasNonEmptyValue)
            {
                if (!isRelated) AddHrefs(h, row, item, hasSelect);
                tbody.InnerHtml.AppendHtml(row);
            }
        }

        return tbody;
    }

    private static IHtmlContent getValue<TModel>(PropertyInfo? p, TModel? item)
    {
        if (p == null || item == null)
        {
            return new HtmlString(string.Empty);
        }

        var v = p.GetValue(item);
        if (v == null)
        {
            return new HtmlString(string.Empty);
        }

        var dt = v as DateTime?;
        if (dt != null)
        {
            return new HtmlString(dt?.ToShortDateString() ?? string.Empty);
        }

        if (p.Name == "Price" && v is double price)
        {
            return new HtmlString($"{price:C}");
        }

        return new HtmlString(v.ToString() ?? string.Empty);
    }

    private static void AddHrefs<TModel>(IHtmlHelper<IEnumerable<TModel>> h, TagBuilder row, TModel item, bool hasSelect)
    {
        var itemId = item.GetType().GetProperty("Id")?.GetValue(item)?.ToString();
        var controllerName = h.ViewContext.RouteData.Values["controller"].ToString();
        var tdActions = new TagBuilder("td");

        if (itemId != null)
        {
            //if (hasSelect)
            //{
            //    tdActions.InnerHtml.AppendHtml(h.ActionLink("Select", "Index", controllerName,
            //        new { selectedId = itemId, pageIdx = h.ViewBag.PageIdx, orderBy = h.ViewBag.OrderBy, filter = h.ViewBag.Filter }));
            //    tdActions.InnerHtml.Append(" | ");
            //}
            tdActions.InnerHtml.AppendHtml($"<a href='/{controllerName}/Edit/{itemId}'>Edit</a> | ");
            tdActions.InnerHtml.AppendHtml($"<a href='/{controllerName}/Details/{itemId}'>Details</a> | ");
            tdActions.InnerHtml.AppendHtml($"<a href='/{controllerName}/Delete/{itemId}'>Delete</a>");
        }

        row.InnerHtml.AppendHtml(tdActions);
    }
}
