using MVC.Core;
using MVC.Data;

namespace MVC.Domain;

public sealed class Medicine(MedicineData? d) : Entity<MedicineData>(d)
{
    public Medicine() : this(null) { }

    public string? RecordNr => data?.RecordNr;
    public string? MedicineName => data?.MedicineName;
    public string? Description => data?.Description;
    public bool? RequiresPrescription => data?.RequiresPrescription;
}