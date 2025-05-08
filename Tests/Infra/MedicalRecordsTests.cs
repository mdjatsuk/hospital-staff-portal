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

[TestClass] public class MedicalRecordsRepoTests
   : RepoBaseTests<MedicalRecordsRepo, MedicalRecord, MedicalRecordData>
{
    protected override MedicalRecord? createEntity(Func<MedicalRecordData> getData)
        => new(getData());
    protected override MedicalRecordsRepo createObj() => new(dbContext!);
}
