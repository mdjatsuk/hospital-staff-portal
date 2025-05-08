using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class MedicineData : EntityData<MedicineData>
{
    public string? RecordNr { get; set; }
    public string? Diagnosis { get; set; }
    public string? Medicine { get; set; }
    public string? Description { get; set; }
    public bool RequiresPrescription{ get; set; }
}