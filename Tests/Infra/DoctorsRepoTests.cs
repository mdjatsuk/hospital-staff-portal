using MVC.Data;
using MVC.Domain;
using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class DoctorsRepoTests
   : RepoBaseTests<DoctorsRepo, Doctor, DoctorData>
{
    protected override Doctor? createEntity(Func<DoctorData> getData)
        => new(getData());
    protected override DoctorsRepo createObj() => new(dbContext!);
}
