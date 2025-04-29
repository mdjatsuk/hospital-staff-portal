using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class AppointmentViewFactoryTests : 
    SealedTests<AppointmentViewFactory,  AbstractViewFactory<AppointmentData, AppointmentView>>
{
    private AppointmentData? data;
    private AppointmentView? view;
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
    private AppointmentView crView()
    {
        var v = new AppointmentView
        {
            Id = 1,
            DoctorId = 2,
            PatientId = 3,
            Date = DateTime.Today,
            Location = "View Location",
            AppointmentFee = 200.0
        };
        return v;
    }
    private AppointmentData crData()
    {
        var d = new AppointmentData
        {
            Id = 1000,
            DoctorId = 4,
            PatientId = 5,
            Date = DateTime.Today.AddDays(-1),
            Location = "Data Location",
            AppointmentFee = 300.0
        };
        return d;
    }
    [TestMethod] public void CreateViewTest()
    {
        var f = new AppointmentViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.DoctorId, v.DoctorId);
        equal(data?.PatientId, v.PatientId);
        equal(data?.Date, v.Date);
        equal(data?.Location, v.Location);
        equal(data?.AppointmentFee, v.AppointmentFee);
    }
    [TestMethod] public void CreateDataTest()
    {
        var f = new AppointmentViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.DoctorId, d.DoctorId);
        equal(view?.PatientId, d.PatientId);
        equal(view?.Date, d.Date);
        equal(view?.Location, d.Location);
        equal(view?.AppointmentFee, d.AppointmentFee);
    }
}
