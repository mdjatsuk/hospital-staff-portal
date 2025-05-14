using MVC.Data;

namespace MVC.Tests.Data;

[TestClass] public class EntityDataTests : AbstractTests<EntityData<DoctorData>, EntityData>
{
    [TestMethod] public void IdTest() => isProperty<int>();
    protected override EntityData<DoctorData> createObj() => new DoctorData();
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        var o = obj as DoctorData;
        o!.Id = 1;
        o!.FirstName = "Bob";
        o!.LastName = "Bobbby";
        o!.Specialization = Specialities.Cardiology;
        o!.PhoneNumber = 87654321;
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal(1, d?.Id);
        equal("Bob", d?.FirstName);
        equal("Bobbby", d?.LastName);
        equal(Specialities.Cardiology, d?.Specialization);
        equal(87654321, d?.PhoneNumber);
    }
}