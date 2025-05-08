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

[TestClass] public class DoctorsRepoTests
   : RepoBaseTests<DoctorsRepo, Doctor, DoctorData>
{
    protected override Doctor? createEntity(Func<DoctorData> getData)
        => new(getData());
    protected override DoctorsRepo createObj() => new(dbContext!);
}
