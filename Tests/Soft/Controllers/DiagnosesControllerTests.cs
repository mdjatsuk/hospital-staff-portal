using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class DiagnosesControllerTests() :
ControllerBaseTests<DiagnosesController, Diagnosis, DiagnosisData, DiagnosisView>
{
    protected override Diagnosis? createEntity(Func<DiagnosisData> getData)
       => new(getData());
    protected override DiagnosesController createObj() => new(dbContext!);

}
