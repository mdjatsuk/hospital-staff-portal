using MVC.Data;
using MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Domain;

[TestClass] public class DoctorTests : BaseClassTests<Doctor, Entity<DoctorData>>
{
    protected override Doctor createObj()
    {
        var d = new DoctorData
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Specialization = Specialties.Cardiology,
            PhoneNumber = "12345678"
        };
        return new Doctor(d);
    }
    [TestMethod] public void FirstNameTest() => equal("John", obj?.FirstName);
    [TestMethod] public void LastNameTest() => equal("Doe", obj?.LastName);
    [TestMethod] public void SpecializationTest() => equal(Specialties.Cardiology, obj?.Specialization);
    [TestMethod] public void PhoneNumberTest() => equal("12345678", obj?.PhoneNumber);
    [TestMethod] public void FullNameTest() => equal("John Doe", obj?.FullName);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
}