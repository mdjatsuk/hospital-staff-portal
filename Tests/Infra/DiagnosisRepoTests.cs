using MVC.Data;
using MVC.Domain;
using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class DiagnosisRepoTests
   : RepoBaseTests<DiagnosisRepo, Diagnosis, DiagnosisData>
{
    protected override Diagnosis? createEntity(Func<DiagnosisData> getData)
        => new(getData());
    protected override DiagnosisRepo createObj() => new(dbContext!);
}
