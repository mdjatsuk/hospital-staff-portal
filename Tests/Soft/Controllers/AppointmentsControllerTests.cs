using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class AppointmentsControllerTests() :
ControllerBaseTests<AppointmentsController, Appointment, AppointmentData, AppointmentView>
{
    protected override Appointment? createEntity(Func<AppointmentData> getData)
       => new(getData());
    protected override AppointmentsController createObj() => new(dbContext!);

}
