using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass]
public class MedicineViewFactoryTests : 
    SealedTests<MedicineViewFactory, AbstractViewFactory<MedicineData, MedicineView>>
{
    private MedicineData? data;
    private MedicineView? view;
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        data = crData();
        view = crView();
    }
    [TestCleanup] public override void Cleanup()
    {
        base.Cleanup();
        data = null;
        view = null;
    }
    private MedicineView crView()
    {
        var v = new MedicineView
        {
            Id = 1,
            MedicineName = "Aspirin",
            Description = "View Description",
        };
        return v;
    }
    private MedicineData crData()
    {
        var d = new MedicineData
        {
            Id = 1000,
            MedicineName = "Aspirin",
            Description = "Data Description",
        };
        return d;
    }
    [TestMethod]
    public void CreateViewTest()
    {
        var f = new MedicineViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.MedicineName, v.MedicineName);
        equal(data?.Description, v.Description);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new MedicineViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.MedicineName, d.MedicineName);
        equal(view?.Description, d.Description);
    }
}
