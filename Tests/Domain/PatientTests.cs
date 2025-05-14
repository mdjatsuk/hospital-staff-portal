using MVC.Data;
using MVC.Domain;

namespace MVC.Tests.Domain;

[TestClass]
public class PatientTests : SealedTests<Patient, Entity<PatientData>>
{
    protected override Patient createObj()
    {
        var d = new PatientData
        {
            Id = 1,
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Genders.Female
        };
        return new Patient(d);
    }
    [TestMethod] public void FirstNameTest() => equal("Jane", obj?.FirstName);
    [TestMethod] public void LastNameTest() => equal("Doe", obj?.LastName);
    [TestMethod] public void DateOfBirthTest() => equal(new DateTime(1990, 1, 1), obj?.DateOfBirth);
    [TestMethod] public void GenderTest() => equal(Genders.Female, obj?.Gender);
    [TestMethod] public void FullNameTest() => equal("Jane Doe", obj?.FullName);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
}
