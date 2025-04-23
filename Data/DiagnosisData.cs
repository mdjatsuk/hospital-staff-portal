using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class DiagnosisData : EntityData<DiagnosisData>
{
    public string? DiagnosisName { get; set; }
    public string? Description { get; set; }
    public bool RequiresSurgery { get; set; }
}