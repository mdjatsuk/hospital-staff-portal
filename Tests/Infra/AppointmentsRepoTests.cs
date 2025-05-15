using MVC.Data;
using MVC.Domain;
using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class AppointmentsRepoTests
   : RepoBaseTests<AppointmentsRepo, Appointment, AppointmentData>
{
    protected override Appointment? createEntity(Func<AppointmentData> getData)
        => new(getData());
    protected override AppointmentsRepo createObj() => new(dbContext!);
}
