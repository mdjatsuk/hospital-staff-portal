using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Controllers;

[TestClass] public class PatientsControllerTests() :
ControllerBaseTests<PatientsController, Patient, PatientData, PatientView>
{
    protected override Patient? createEntity(Func<PatientData> getData)
       => new(getData());
    protected override PatientsController createObj() => new(dbContext!);

}
