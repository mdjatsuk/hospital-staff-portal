using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class DoctorsControllerTests() :
ControllerBaseTests<DoctorsController, Doctor, DoctorData, DoctorView>
{
    protected override Doctor? createEntity(Func<DoctorData> getData)
       => new(getData());
    protected override DoctorsController createObj() => new(dbContext!);

}
