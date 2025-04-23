using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class DiagnosisData : EntityData<DiagnosisData>
{
    public string? Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public double Price { get; set; }

    public string? Genre { get; set; }

    public string? Rating { get; set; }
}