using MVC.Infra;

namespace MVC.Tests.Infra;

[TestClass] public class MedicalRecordsRepoTests
   : RepoBaseTests<MedicalRecordsRepo, MedicalRecord, MedicalRecordData>
{
    protected override MedicalRecord? createEntity(Func<MedicalRecordData> getData)
        => new(getData());
    protected override MedicalRecordsRepo createObj() => new(dbContext!);
}
