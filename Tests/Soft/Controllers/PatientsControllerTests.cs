using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class PatientsControllerTests() :
ControllerBaseTests<PatientsController, Patient, PatientData, PatientView>
{
    protected override Patient? createEntity(Func<PatientData> getData)
       => new(getData());
    protected override PatientsController createObj() => new(dbContext!);

}
