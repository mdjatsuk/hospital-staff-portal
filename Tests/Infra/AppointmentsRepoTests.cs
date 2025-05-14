using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Infra;

[TestClass] public class AppointmentsRepoTests
   : RepoBaseTests<AppointmentsRepo, Appointment, AppointmentData>
{
    protected override Appointment? createEntity(Func<AppointmentData> getData)
        => new(getData());
    protected override AppointmentsRepo createObj() => new(dbContext!);
}
