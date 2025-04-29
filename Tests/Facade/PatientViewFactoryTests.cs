using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class PatientViewFactoryTests : 
    SealedTests<PatientViewFactory, AbstractViewFactory<PatientData, PatientView>>
{
    private PatientData? data;
    private PatientView? view;
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
    private PatientView crView()
    {
        var v = new PatientView
        {
            Id = 1,
            FirstName = "View First Name",
            LastName = "View Last Name",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Genders.Female,
        };
        return v;
    }
    private PatientData crData()
    {
        var d = new PatientData
        {
            Id = 1000,
            FirstName = "Data First Name",
            LastName = "Data Last Name",
            DateOfBirth = new DateTime(1980, 1, 1),
            Gender = Genders.Male,
        };
        return d;
    }
    [TestMethod] public void CreateViewTest()
    {
        var f = new PatientViewFactory();
        var v = f.CreateView(data);
        notNull(v);
        equal(data?.Id, v.Id);
        equal(data?.FirstName, v.FirstName);
        equal(data?.LastName, v.LastName);
        equal(data?.DateOfBirth, v.DateOfBirth);
        equal(data?.Gender, v.Gender);
    }
    [TestMethod] public void CreateDataTest()
    {
        var f = new PatientViewFactory();
        var d = f.CreateData(view);
        notNull(d);
        equal(view?.Id, d.Id);
        equal(view?.FirstName, d.FirstName);
        equal(view?.LastName, d.LastName);
        equal(view?.DateOfBirth, d.DateOfBirth);
        equal(view?.Gender, d.Gender);
    }
}
