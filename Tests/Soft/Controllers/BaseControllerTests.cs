using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass]
public class BaseControllerTests() :
ControllerBaseTests<BaseController<Doctor, DoctorData, DoctorView>, Doctor, DoctorData, DoctorView>
{
    protected override BaseController<Doctor, DoctorData, DoctorView> createObj() => new DoctorsController(dbContext!);
    [TestMethod]
    public override void IsSealedTest() =>
        isFalse(typeof(BaseController<Doctor, DoctorData, DoctorView>).IsSealed);
    [TestMethod]
    public void IsAbstractTest() =>
        isTrue(typeof(BaseController<Doctor, DoctorData, DoctorView>).IsAbstract);
    [TestMethod]
    public override void IsBaseTypeOfTest() =>
        equal(typeof(BaseController<Doctor, DoctorData, DoctorView>).BaseType, typeof(Controller));
    protected override Doctor? createEntity(Func<DoctorData> getData) => new(getData());
}
