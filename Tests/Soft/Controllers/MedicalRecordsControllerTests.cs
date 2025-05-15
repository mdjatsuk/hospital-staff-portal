using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class MedicalRecordsControllerTests() :
ControllerBaseTests<MedicalRecordsController, MedicalRecord, MedicalRecordData, MedicalRecordView>
{
    protected override MedicalRecord? createEntity(Func<MedicalRecordData> getData)
       => new(getData());
    protected override MedicalRecordsController createObj() => new(dbContext!);

}
