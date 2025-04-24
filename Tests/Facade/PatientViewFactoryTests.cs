using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade;

[TestClass] public class PatientViewFactoryTests : BaseTests
{
    private PatientData? data;
    private PatientView? view;
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
    private PatientView crView()
    {
        var v = new PatientView
        {
            Id = 1,
            FirstName = "View First Name",
            LastName = "View Last Name",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Genders.Female,
            DiagnosisId = 2,
            Diagnosis = "View Diagnosis"
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
            DiagnosisId = 3
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
        equal(data?.DiagnosisId, v.DiagnosisId);
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
        equal(view?.DiagnosisId, d.DiagnosisId);
    }
}
