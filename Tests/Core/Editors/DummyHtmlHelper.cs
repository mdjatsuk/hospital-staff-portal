using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewContext = Microsoft.AspNetCore.Mvc.Rendering.ViewContext;
using Html5DateRenderingMode = Microsoft.AspNetCore.Mvc.Rendering.Html5DateRenderingMode;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using ViewDataDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary;
using FormMethod = Microsoft.AspNetCore.Mvc.Rendering.FormMethod;

namespace MVC.Tests.Core.Editors;

public class DummyHtmlHelper<TModel> : IHtmlHelper<TModel>
{
    public IHtmlContent EditorFor<TValue>(Expression<Func<TModel, TValue>> expression, object additionalViewData)
        => new HtmlString("<input type='checkbox' class='form-check-input' />");
    public IHtmlContent EditorFor<TResult>(Expression<Func<TModel, TResult>> expression,
        string templateName, string htmlFieldName, object additionalViewData)
        => new HtmlString("<input type='checkbox' class='form-check-input' />");
    public IHtmlContent ValidationMessageFor<TValue>(Expression<Func<TModel, TValue>> expression,
        string message, object htmlAttributes)
        => new HtmlString("<span class='text-danger'>Error</span>");
    public IHtmlContent ValidationMessageFor<TValue>(Expression<Func<TModel, TValue>> expression,
        string message, object htmlAttributes, string tag)
        => new HtmlString("<span class='text-danger'>Error</span>");
    public IHtmlContent ForShow<TValue>(Expression<Func<TModel, TValue>> expression, params IHtmlContent[] contents)
        => new HtmlString("Dummy ForShow Result");
    public IHtmlContent DropDownListFor<TResult>(Expression<Func<TModel, TResult>> expression,
        IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
    {
        var options = string.Join("", selectList.Select(item => $"<option>{item.Text}</option>"));
        return new HtmlString($"<select class='form-select'>{options}</select>");
    }
    public IHtmlContent DropDownListFor<TResult>(Expression<Func<TModel, TResult>> expression,
        IEnumerable<SelectListItem> selectList, object htmlAttributes)
    {
        var options = string.Join("", selectList.Select(item => $"<option>{item.Text}</option>"));
        return new HtmlString($"<select class='form-control'>{options}</select>");
    }
    public IHtmlContent LabelFor<TResult>(Expression<Func<TModel, TResult>> expression, object htmlAttributes)
        => new HtmlString($"<label class=\"control-label\" for=\"CategoryId\">CategoryId</label>");
    public IHtmlContent LabelFor<TResult>(Expression<Func<TModel, TResult>> expression, string labelText, object htmlAttributes)
    {
        var name = "CategoryId";
        var text = string.IsNullOrEmpty(labelText) ? name : labelText;
        return new HtmlString($"<label class=\"control-label\" for=\"{name}\">{text}</label>");
    }
    public string NameFor<TValue>(Expression<Func<TModel, TValue>> expression) => "CategoryId";
    public string ValueFor<TValue>(Expression<Func<TModel, TValue>> expression, string format) => "42";
    public IHtmlContent DisplayFor<TValue>(Expression<Func<TModel, TValue>> expression) => new HtmlString("Dummy ForShow Result");
    public IHtmlContent DisplayFor<TValue>(Expression<Func<TModel, TValue>> expression, string templateName,
        string htmlFieldName, object additionalViewData) => new HtmlString("Dummy ForShow Result");
    public dynamic ViewBag { get; set; } = new System.Dynamic.ExpandoObject();
    public ViewContext ViewContext => new ViewContext
    {
        RouteData = new Microsoft.AspNetCore.Routing.RouteData()
    };
    public IHtmlContent Raw(string value) => new HtmlString(value);
    public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, string protocol,
        string hostname, string fragment, object routeValues, object htmlAttributes)
        => new HtmlString($"<a href='/{controllerName}/{actionName}'>{linkText}</a>");
    public IHtmlContent ActionLink(string linkText, string actionName, string controllerName,
        object routeValues, object htmlAttributes)
        => new HtmlString($"<a href='/{controllerName}/{actionName}'>{linkText}</a>");
    public void Contextualize(ViewContext viewContext) { }
    #region NotImplementedMembers
    public IViewDataContainer ViewDataContainer => throw new NotImplementedException();
    public ITempDataDictionary TempData => throw new NotImplementedException();
    public ViewDataDictionary ViewData => throw new NotImplementedException();
    Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TModel> IHtmlHelper<TModel>.ViewData => throw new NotImplementedException();
    public Html5DateRenderingMode Html5DateRenderingMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string IdAttributeDotReplacement => throw new NotImplementedException();
    public IModelMetadataProvider MetadataProvider => throw new NotImplementedException();
    public UrlEncoder UrlEncoder => throw new NotImplementedException();
    public IHtmlContent Partial(string partialViewName, object model, ViewDataDictionary viewData) => throw new NotImplementedException();
    public IHtmlContent DisplayForModel(string templateName, object additionalViewData) => throw new NotImplementedException();
    public string Encode(string value) => throw new NotImplementedException();
    public string Encode(object value) => throw new NotImplementedException();
    public string FormatValue(object value, string format) => throw new NotImplementedException();
    public string IdFor<TValue>(Expression<Func<TModel, TValue>> expression) => throw new NotImplementedException();
    public IHtmlContent ValidationMessage(string modelName, string message, object htmlAttributes, string tag) => throw new NotImplementedException();
    public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes, string tag) => throw new NotImplementedException();
    public Task<IHtmlContent> PartialAsync(string partialViewName, object model, ViewDataDictionary viewData) => throw new NotImplementedException();
    public Task<IHtmlContent> EditorForAsync<TValue>(Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public Task<IHtmlContent> DisplayForAsync<TValue>(Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public Task<IHtmlContent> PartialAsync(string partialViewName, object model) => throw new NotImplementedException();
    public IHtmlContent CheckBoxFor(Expression<Func<TModel, bool>> expression, object htmlAttributes) => throw new NotImplementedException();
    public string DisplayNameFor<TResult>(Expression<Func<TModel, TResult>> expression) => "Dummy Display Name";
    public string DisplayNameForInnerType<TModelItem, TResult>(Expression<Func<TModelItem, TResult>> expression) => throw new NotImplementedException();
    public string DisplayTextFor<TResult>(Expression<Func<TModel, TResult>> expression) => throw new NotImplementedException();
    public IHtmlContent HiddenFor<TResult>(Expression<Func<TModel, TResult>> expression, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent ListBoxFor<TResult>(Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent PasswordFor<TResult>(Expression<Func<TModel, TResult>> expression, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent RadioButtonFor<TResult>(Expression<Func<TModel, TResult>> expression, object value, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent Raw(object value) => throw new NotImplementedException();
    public IHtmlContent TextAreaFor<TResult>(Expression<Func<TModel, TResult>> expression, int rows, int columns, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent TextBoxFor<TResult>(Expression<Func<TModel, TResult>> expression, string format, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent AntiForgeryToken() => throw new NotImplementedException();
    public MvcForm BeginForm(string actionName, string controllerName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes) => throw new NotImplementedException();
    public MvcForm BeginRouteForm(string routeName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent Display(string expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public string DisplayName(string expression) => throw new NotImplementedException();
    public string DisplayText(string expression) => throw new NotImplementedException();
    public IHtmlContent DropDownList(string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent Editor(string expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public void EndForm() => throw new NotImplementedException();
    public string GenerateIdFromName(string fullName) => throw new NotImplementedException();
    public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct => throw new NotImplementedException();
    public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType) => throw new NotImplementedException();
    public IHtmlContent Hidden(string expression, object value, object htmlAttributes) => throw new NotImplementedException();
    public string Id(string expression) => throw new NotImplementedException();
    public IHtmlContent Label(string expression, string labelText, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) => throw new NotImplementedException();
    public string Name(string expression) => throw new NotImplementedException();
    public IHtmlContent Password(string expression, object value, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes) => throw new NotImplementedException();
    public Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData) => throw new NotImplementedException();
    public IHtmlContent RouteLink(string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes) => throw new NotImplementedException();
    public string Value(string expression, string format) => throw new NotImplementedException();
    #endregion
}



public class DummyHtmlHelperIEnumerable<TModel> : IHtmlHelper<IEnumerable<TModel>>
{
    public ViewContext ViewContext => new ViewContext
    {
        RouteData = new Microsoft.AspNetCore.Routing.RouteData()
        {
            Values = { ["controller"] = "TestController" }
        }
    };

    public IHtmlContent Raw(string value) => new HtmlString(value);
    public dynamic ViewBag { get; } = new System.Dynamic.ExpandoObject();
    public string Encode(string value) => value;
    public string Encode(object value) => value?.ToString();
    public IHtmlContent DisplayFor<TValue>(Expression<Func<IEnumerable<TModel>, TValue>> expression)
        => new HtmlString("DisplayFor dummy");
    
    public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, string protocol,
        string hostname, string fragment, object routeValues, object htmlAttributes)
        => new HtmlString($"<a href='/{controllerName}/{actionName}'>{linkText}</a>");
    public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, object routeValues,
        object htmlAttributes)
    {
        var query = string.Join("&", routeValues
        .GetType()
        .GetProperties()
        .Select(p => $"{p.Name}={p.GetValue(routeValues)}"));
        return new HtmlString($"<a href='/{controllerName}/{actionName}?{query}'>{linkText}</a>");
    }
    #region Not Implemented
    public IHtmlContent Partial(string partialViewName, object model, ViewDataDictionary viewData)
        => throw new NotImplementedException();
    public IHtmlContent DisplayForModel(string templateName, object additionalViewData)
        => throw new NotImplementedException();
    public string IdAttributeDotReplacement => throw new NotImplementedException();
    public IModelMetadataProvider MetadataProvider => throw new NotImplementedException();
    public UrlEncoder UrlEncoder => throw new NotImplementedException();
    public IViewDataContainer ViewDataContainer => throw new NotImplementedException();
    public ViewDataDictionary ViewData => throw new NotImplementedException();
    public ITempDataDictionary TempData => throw new NotImplementedException();
    Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IEnumerable<TModel>> IHtmlHelper<IEnumerable<TModel>>.ViewData => throw new NotImplementedException();
    public Html5DateRenderingMode Html5DateRenderingMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public void Contextualize(ViewContext viewContext) => throw new NotImplementedException();
    public IHtmlContent CheckBoxFor(Expression<Func<IEnumerable<TModel>, bool>> expression, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent DisplayFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public string DisplayNameFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression) => throw new NotImplementedException();
    public string DisplayNameForInnerType<TModelItem, TResult>(Expression<Func<TModelItem, TResult>> expression) => throw new NotImplementedException();
    public string DisplayTextFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression) => throw new NotImplementedException();
    public IHtmlContent DropDownListFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent EditorFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public IHtmlContent HiddenFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, object htmlAttributes) => throw new NotImplementedException();
    public string IdFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression) => throw new NotImplementedException();
    public IHtmlContent LabelFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, string labelText, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent ListBoxFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) => throw new NotImplementedException();
    public string NameFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression) => throw new NotImplementedException();
    public IHtmlContent PasswordFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent RadioButtonFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, object value, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent Raw(object value) => throw new NotImplementedException();
    public IHtmlContent TextAreaFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, int rows, int columns, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent TextBoxFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, string format, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent ValidationMessageFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, string message, object htmlAttributes, string tag) => throw new NotImplementedException();
    public string ValueFor<TResult>(Expression<Func<IEnumerable<TModel>, TResult>> expression, string format) => throw new NotImplementedException();
    public IHtmlContent AntiForgeryToken() => throw new NotImplementedException();
    public MvcForm BeginForm(string actionName, string controllerName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes) => throw new NotImplementedException();
    public MvcForm BeginRouteForm(string routeName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent Display(string expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public string DisplayName(string expression) => throw new NotImplementedException();
    public string DisplayText(string expression) => throw new NotImplementedException();
    public IHtmlContent DropDownList(string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent Editor(string expression, string templateName, string htmlFieldName, object additionalViewData) => throw new NotImplementedException();
    public void EndForm() => throw new NotImplementedException();
    public string FormatValue(object value, string format) => throw new NotImplementedException();
    public string GenerateIdFromName(string fullName) => throw new NotImplementedException();
    public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct => throw new NotImplementedException();
    public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType) => throw new NotImplementedException();
    public IHtmlContent Hidden(string expression, object value, object htmlAttributes) => throw new NotImplementedException();
    public string Id(string expression) => throw new NotImplementedException();
    public IHtmlContent Label(string expression, string labelText, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) => throw new NotImplementedException();
    public string Name(string expression) => throw new NotImplementedException();
    public Task<IHtmlContent> PartialAsync(string partialViewName, object model, ViewDataDictionary viewData) => throw new NotImplementedException();
    public IHtmlContent Password(string expression, object value, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes) => throw new NotImplementedException();
    public Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData) => throw new NotImplementedException();
    public IHtmlContent RouteLink(string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes) => throw new NotImplementedException();
    public IHtmlContent ValidationMessage(string expression, string message, object htmlAttributes, string tag) => throw new NotImplementedException();
    public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes, string tag) => throw new NotImplementedException();
    public string Value(string expression, string format) => throw new NotImplementedException();
    #endregion
}