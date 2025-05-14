namespace MVC.Facade;

public sealed class MedicalRecordViewFactory : AbstractViewFactory<MedicalRecordData, MedicalRecordView> 
{
    public override async Task<MedicalRecordView> CreateView(MedicalRecordData? d, bool loadLazy = false)
    {
        var v = await base.CreateView(d, loadLazy);
        if (!loadLazy) return v;
        var o = new MedicalRecord(d);
        await o.LoadLazy();
        v.Patient = o.Patient?.FullName;
        v.RecordNr = o.RecordNr?.RecordNr;
        return v;
    }
}