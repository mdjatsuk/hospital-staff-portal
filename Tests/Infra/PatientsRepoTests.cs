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

[TestClass] public class PatientsRepoTests
   : RepoBaseTests<PatientsRepo, Patient, PatientData>
{
    protected override Patient? createEntity(Func<PatientData> getData)
        => new(getData());
    protected override PatientsRepo createObj() => new(dbContext!);
}
