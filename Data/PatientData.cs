namespace MVC.Data;

public sealed class PatientData : EntityData<PatientData>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Genders? Gender { get; set; }
}