using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVC.Tests.Core;

[TestClass] public class TypeHelperTests : BaseTests
{
    protected override Type setType() => typeof(TypeHelper);
    [DisplayName("Custom Display Name")] private class CustomNamedClass { }
    private class DefaultNamedClass { }
    [TestMethod] public void WithTypeWithAttribute_ReturnsCustomName()
    {
        var result = TypeHelper.DisplayName(typeof(CustomNamedClass));
        equal("Custom Display Name", result);
    }
    [TestMethod] public void WithTypeWithoutAttribute_ReturnsTypeName()
    {
        var result = TypeHelper.DisplayName(typeof(DefaultNamedClass));
        equal("DefaultNamedClass", result);
    }
    [TestMethod] public void WithNullTypeWithDefault_ReturnsDefaultName()
    {
        var result = TypeHelper.DisplayName((Type?)null, "Fallback");
        equal("Fallback", result);
    }
    [TestMethod] public void WithNullTypeWithoutDefault_ReturnsEmptyString()
    {
        var result = TypeHelper.DisplayName((Type?)null);
        equal(string.Empty, result);
    }
    [TestMethod] public void WithObjectWithAttribute_ReturnsCustomName()
    {
        var result = TypeHelper.DisplayName(new CustomNamedClass());
        equal("Custom Display Name", result);
    }
    [TestMethod] public void WithObjectWithoutAttribute_ReturnsTypeName()
    {
        var result = TypeHelper.DisplayName(new DefaultNamedClass());
        equal("DefaultNamedClass", result);
    }
    [TestMethod] public void WithNullObjectWithDefault_ReturnsDefaultName()
    {
        var result = TypeHelper.DisplayName((object?)null, "Default");
        equal("Default", result);
    }
    [TestMethod] public void WithGenericTypeWithAttribute_ReturnsCustomName()
    {
        var list = new List<CustomNamedClass>();
        var result = TypeHelper.DisplayName(list);
        equal("Custom Display Name", result);
    }
    [TestMethod] public void WithGenericTypeWithoutAttribute_ReturnsTypeName()
    {
        var list = new List<DefaultNamedClass>();
        var result = TypeHelper.DisplayName(list);
        equal("DefaultNamedClass", result);
    }
    [TestMethod] public void WithGenericTypeAndNullDefault_ReturnsTypeName()
    {
        var result = TypeHelper.DisplayName<List<string>>(null);
        equal("List`1", result);
    }
    [TestMethod] public void DisplayNameTest()
    {
        WithTypeWithAttribute_ReturnsCustomName();
        WithTypeWithoutAttribute_ReturnsTypeName();
        WithNullTypeWithDefault_ReturnsDefaultName();
        WithNullTypeWithoutDefault_ReturnsEmptyString();
        WithObjectWithAttribute_ReturnsCustomName();
        WithObjectWithoutAttribute_ReturnsTypeName();
        WithNullObjectWithDefault_ReturnsDefaultName();
        WithGenericTypeWithAttribute_ReturnsCustomName();
        WithGenericTypeWithoutAttribute_ReturnsTypeName();
        WithGenericTypeAndNullDefault_ReturnsTypeName();
    }
}