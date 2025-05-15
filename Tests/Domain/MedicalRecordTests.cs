using MVC.Core;
using MVC.Data;
using MVC.Domain;
using Random = MVC.Aids.Random;

namespace MVC.Tests.Domain;

[TestClass] public class MedicalRecordTests : SealedTests<MedicalRecord, Entity<MedicalRecordData>>
{
    MedicalRecordData? data = null;

    protected override MedicalRecord createObj()
    {
        data = Random.Object<MedicalRecordData>();
        return new MedicalRecord(data);
    }


    [TestMethod] public void PatientIdTest() => isReadOnly(data!.PatientId);
    [TestMethod] public void RecordNrIdTest() => isReadOnly(data!.RecordNrId);
    [TestMethod] public void DiagnosedOnTest() => isReadOnly(data!.DiagnosedOn);
    [TestMethod] public void PatientTest() => isReadOnly<Patient>(null);
    [TestMethod] public void RecordNrTest() => isReadOnly<Diagnosis>(null);


    [TestMethod]
    public async Task LoadLazyTest()
    {
        var patientRepo = new mockPatientRepo();
        var patientData = Random.Object<PatientData>();
        patientData.Id = data!.PatientId;
        var patient = new Patient(patientData);
        patientRepo.list.Add(patient);
        for (var i = 0; i < Random.UInt8(5, 10); i++)
            patientRepo.list.Add(new Patient(Random.Object<PatientData>()));
        Services.services.Add(typeof(IPatientsRepo), patientRepo);

        var diagnosisRepo = new mockDiagnosisRepo();
        var diagnosisData = Random.Object<DiagnosisData>();
        diagnosisData.Id = data!.RecordNrId;
        var diagnosis = new Diagnosis(diagnosisData);
        diagnosisRepo.list.Add(diagnosis);
        for (var i = 0; i < Random.UInt8(5, 10); i++)
            diagnosisRepo.list.Add(new Diagnosis(Random.Object<DiagnosisData>()));
        Services.services.Add(typeof(IDiagnosisRepo), diagnosisRepo);

        await obj!.LoadLazy();

        equal(patient.Id, obj.Patient?.Id);
        equal(diagnosis.Id, obj.RecordNr?.Id);
    }
}
