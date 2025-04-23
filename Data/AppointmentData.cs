using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class AppointmentData : EntityData<AppointmentData>
{
    public DateTime? Date { get; set; }

    public string? Location { get; set; }

    public double AppointmentFee { get; set; }
}