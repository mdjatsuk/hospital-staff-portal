using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class DoctorDataTests : SealedTests<DoctorData, EntityData<DoctorData>>
{
    [TestMethod] public void FirstNameTest() => isProperty<string>();
    [TestMethod] public void LastNameTest() => isProperty<string>();
    [TestMethod] public void SpecializationTest() => isProperty<Specialities?>();
    [TestMethod] public void PhoneNumberTest() => isProperty<long?>();
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.FirstName = "John";
        obj.LastName = "Doe";
        obj.Specialization = Specialities.Cardiology;
        obj.PhoneNumber = 12345678;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal("John", d?.FirstName);
        equal("Doe", d?.LastName);
        equal(Specialities.Cardiology, d?.Specialization);
        equal(12345678, d?.PhoneNumber);
    }
}
