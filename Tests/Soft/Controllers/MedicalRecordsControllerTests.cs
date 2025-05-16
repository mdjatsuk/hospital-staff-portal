using MVC.Data;
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
    protected internal override MedicalRecordView createView()
    {
        var view = base.createView();
        var diagnosisId = 123;
        var recordNr = "testrecnr";
        view.RecordNrId = diagnosisId;
        view.RecordNr = recordNr;
        var diagnosis = new DiagnosisData
        {
            Id = diagnosisId,
            RecordNr = recordNr
        };
        dbContext!.Diagnoses.Add(diagnosis);
        dbContext!.SaveChanges();
        return view;
    }
    protected override MedicalRecordData createData()
    {
        var data = base.createData();
        data.RecordNrId = 123;
        data.RecordNr = "testrecnr";
        return data;
    }
    protected internal override void validate(MedicalRecordData? d, MedicalRecordView v)
    {
        var validated = 0;
        foreach (var pi in d!.GetType().GetProperties())
        {
            if (pi.Name == nameof(MedicalRecordData.RecordNr) ||
                pi.Name == nameof(MedicalRecordData.PatientFullName))
                continue;
            var actual = pi.GetValue(d);
            var vpi = v.GetType().GetProperty(pi.Name);
            if (vpi is null) continue;
            var expected = vpi.GetValue(v);
            equal(actual, expected);
            ++validated;
        }
    }
}
