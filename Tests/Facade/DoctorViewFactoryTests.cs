using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class DoctorViewFactoryTests : 
    SealedTests<DoctorViewFactory, AbstractViewFactory<DoctorData, DoctorView>>
{
    private DoctorData? data;
    private DoctorView? view;
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
    private DoctorView crView()
    {
        var v = new DoctorView
        {
            Id = 1,
            FirstName = "View First Name",
            LastName = "View Last Name",
            Specialization = Specialties.Cardiology,
            PhoneNumber = 12345678
        };
        return v;
    }
    private DoctorData crData()
    {
        var d = new DoctorData
        {
            Id = 1000,
            FirstName = "Data First Name",
            LastName = "Data Last Name",
            Specialization = Specialties.Neurology,
            PhoneNumber = 87654321
        };
        return d;
    }
    [TestMethod] public void CreateViewTest()
    {
        var f = new DoctorViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.FirstName, v.FirstName);
        equal(data?.LastName, v.LastName);
        equal(data?.Specialization, v.Specialization);
        equal(data?.PhoneNumber, v.PhoneNumber);
    }
    [TestMethod] public void CreateDataTest()
    {
        var f = new DoctorViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.FirstName, d.FirstName);
        equal(view?.LastName, d.LastName);
        equal(view?.Specialization, d.Specialization);
        equal(view?.PhoneNumber, d.PhoneNumber);
    }
}
