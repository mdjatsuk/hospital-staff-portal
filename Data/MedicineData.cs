using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class MedicineData : EntityData<MedicineData>
{
    public string? MedicineName { get; set; }
    public string? Description { get; set; }
    public bool RequiresPrescription{ get; set; }
}