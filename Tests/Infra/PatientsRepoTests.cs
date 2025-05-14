using MVC.Data;
using MVC.Domain;
using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class PatientsRepoTests
   : RepoBaseTests<PatientsRepo, Patient, PatientData>
{
    protected override Patient? createEntity(Func<PatientData> getData)
        => new(getData());
    protected override PatientsRepo createObj() => new(dbContext!);
}
