using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class DiagnosisViewFactoryTests : BaseTests
{
    private DiagnosisData? data;
    private DiagnosisView? view;
    [TestInitialize] public void TestInitialize()
    {
        data = crData();
        view = crView();
    }
    [TestCleanup] public void TestCleanup()
    {
        data = null;
        view = null;
    }
    private DiagnosisView crView()
    {
        var v = new DiagnosisView
        {
            Id = 1,
            DiagnosisName = DiagnosisEnum.Anemia,
            Description = "View Description",
            RequiresSurgery = true
        };
        return v;
    }
    private DiagnosisData crData()
    {
        var d = new DiagnosisData
        {
            Id = 1000,
            DiagnosisName = DiagnosisEnum.Anemia,
            Description = "Data Description",
            RequiresSurgery = false
        };
        return d;
    }
    [TestMethod] public void CreateViewTest()
    {
        var f = new DiagnosisViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.DiagnosisName, v.DiagnosisName);
        equal(data?.Description, v.Description);
        equal(data?.RequiresSurgery, v.RequiresSurgery);
    }
    [TestMethod] public void CreateDataTest()
    {
        var f = new DiagnosisViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.DiagnosisName, d.DiagnosisName);
        equal(view?.Description, d.Description);
        equal(view?.RequiresSurgery, d.RequiresSurgery);
    }
}
