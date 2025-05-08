using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class MedicalRecordViewFactoryTests : SealedTests<MedicalRecordViewFactory, AbstractViewFactory<MedicalRecordData, MedicalRecordView>>
{
    private MedicalRecordData? data;
    private MedicalRecordView? view;
    [TestInitialize]
    public override void Initialize()
    {
        base.Initialize();
        data = crData();
        view = crView();
    }
    [TestCleanup]
    public override void Cleanup()
    {
        base.Cleanup();
        data = null;
        view = null;
    }
    private MedicalRecordView crView()
    {
        var v = new MedicalRecordView
        {
            Id = 5,
            PatientId = 9,
            Patient = "Liam Thompson",
            DiagnosedOn = DateTime.Today.AddDays(-1),
            PatientFullName = "Liam Thompson"
        };
        return v;
    }
    private MedicalRecordData crData()
    {
        var d = new MedicalRecordData
        {
            Id = 10,
            PatientId = 4,
            DiagnosedOn = DateTime.Today,
            PatientFullName = "Liam Thompson"
        };
        return d;
    }
    [TestMethod] public void CreateDataTest()
    {
        var f = new MedicalRecordViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.PatientId, v.PatientId);
        equal(data?.DiagnosedOn, v.DiagnosedOn);
        equal(data?.PatientFullName, v.PatientFullName);
    }
    [TestMethod] public void CreateViewTest()
    {
        var f = new MedicalRecordViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.PatientId, d.PatientId);
        equal(view?.DiagnosedOn, d.DiagnosedOn);
        equal(view?.PatientFullName, d.PatientFullName);
    }
}