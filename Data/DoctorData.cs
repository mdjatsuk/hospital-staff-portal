using MVC.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Data;

public sealed class DoctorData : EntityData<DoctorData>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Specialities? Specialization { get; set; }
    public long? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
}