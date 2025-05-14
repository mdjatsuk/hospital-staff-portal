using MVC.Data;
using MVC.Facade;

namespace MVC.Tests.Facade;

[TestClass]
public class DiagnosisViewFactoryTests : 
    SealedTests<DiagnosisViewFactory, AbstractViewFactory<DiagnosisData, DiagnosisView>>
{
    private DiagnosisData? data;
    private DiagnosisView? view;
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
    private DiagnosisView crView()
    {
        var v = new DiagnosisView
        {
            Id = 1,
            Medicine = "Aspirin",
            Description = "View Description",
        };
        return v;
    }
    private DiagnosisData crData()
    {
        var d = new DiagnosisData
        {
            Id = 1000,
            Medicine = "Aspirin",
            Description = "Data Description",
        };
        return d;
    }
    [TestMethod]
    public void CreateViewTest()
    {
        var f = new DiagnosisViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.Medicine, v.Medicine);
        equal(data?.Description, v.Description);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new DiagnosisViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.Medicine, d.Medicine);
        equal(view?.Description, d.Description);
    }
}
